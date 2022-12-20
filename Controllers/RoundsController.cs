using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeForARound.Dto;
using TimeForARound.Services;

namespace TimeForARound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoundsController : ControllerBase
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;

    public RoundsController(IMapper mapper, IUserRepo userRepo)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<RoundAloneDto>> GetAllRounds()
    {
        var rounds = _userRepo.GetAllRounds();
        return Ok(_mapper.Map<IEnumerable<RoundAloneDto>>(rounds));
    }
}