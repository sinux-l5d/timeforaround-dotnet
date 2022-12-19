using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeForARound.Dto;
using TimeForARound.Entities;
using TimeForARound.Services;

namespace TimeForARound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper, IUserRepo userRepo)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetAllUsers()
    {
        var users = _userRepo.GetAllUsers();
        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }
    
    [HttpGet("{username}")]
    public ActionResult<UserDetailedDto> GetUserByName(string username)
    {
        var user = _userRepo.GetUser(username);
        if (user == null)
        {
            return NotFound(new { error = $"User \"{username}\" not found" });
        }
        return Ok(_mapper.Map<UserDetailedDto>(user));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<UserDto> CreateUser(UserRegisterDto userRegisterDto)
    {
        var user = _mapper.Map<User>(userRegisterDto);
        if (_userRepo.GetUser(userRegisterDto.Username) != null)
        {
            return Conflict();
        }
        var userDb = _userRepo.AddUser(user);
        return CreatedAtAction(nameof(GetUserByName), new { username = userDb.Username }, _mapper.Map<UserDto>(userDb));
    }
    
    [HttpPost("{username}/rounds")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RoundDto> AddRound([FromRoute] string username, [FromBody] RoundNewDto roundNewDto)
    {
        var userDb = _userRepo.GetUser(username);
        if (userDb == null)
        {
            return NotFound(new { error = $"User \"{username}\" not found" });
        }
        var round = _mapper.Map<Round>(roundNewDto);
        round.Username = username;
        round.ReportedAt = DateTime.Now;
        var roundDb = _userRepo.AddRound(round);
        return CreatedAtAction(nameof(GetRound), new { username, roundId = roundDb.Id }, _mapper.Map<RoundDto>(roundDb));
    }
    
    [HttpGet("{username}/rounds/{roundId:guid}")]
    public ActionResult<RoundDto> GetRound(string username, Guid roundId)
    {
        var userDb = _userRepo.GetUser(username);
        if (userDb == null)
        {
            return NotFound(new { error = "User not found" });
        }
        var roundDb = userDb.Rounds.FirstOrDefault(r => r.Id == roundId);
        if (roundDb == null)
        {
            return NotFound(new { error = $"Round not found or don't belong to user {username}" });
        }
        return Ok(_mapper.Map<RoundDto>(roundDb));
    }
    
    [HttpPatch("{username}/rounds/{roundId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateRound(string username, Guid roundId, RoundUpdateDto roundUpdateDto)
    {
        var userDb = _userRepo.GetUser(username);
        if (userDb == null)
        {
            return NotFound(new { error = "User not found" });
        }
        var roundDb = userDb.Rounds.FirstOrDefault(r => r.Id == roundId);
        if (roundDb == null)
        {
            return NotFound(new { error = $"Round not found or don't belong to user {username}" });
        }
        _userRepo.SetPaid(roundDb.Id, roundUpdateDto.AsBeenPaid);
        return NoContent();
    }
}