using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeForARound.Dto;
using TimeForARound.Entities;
using TimeForARound.Services;

namespace TimeForARound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;

    public UserController(ILogger<UserController> logger, IMapper mapper, IUserRepo userRepo)
    {
        _logger = logger;
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
    public ActionResult<UserDto> GetUserById(string username)
    {
        var user = _userRepo.GetUser(username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost(Name = "CreateUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<UserDto> Post(UserRegisterDto userRegisterDto)
    {
        var user = _mapper.Map<User>(userRegisterDto);
        if (_userRepo.GetUser(userRegisterDto.Username) != null)
        {
            return Conflict();
        }
        var userDb = _userRepo.AddUser(user);
        return CreatedAtAction(nameof(GetUserById), new { username = userDb.Username }, _mapper.Map<UserDto>(userDb));
    }
}