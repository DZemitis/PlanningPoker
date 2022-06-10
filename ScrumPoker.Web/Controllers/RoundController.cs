using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Web.Models.Models.WebRequest;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class RoundController : ControllerBase
{
    private readonly IRoundService _roundService;
    private readonly IMapper _mapper;
    private readonly ILogger<RoundController> _logger;

    public RoundController(IRoundService roundService, IMapper mapper, ILogger<RoundController> logger)
    {
        _roundService = roundService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetRoundById(int id)
    {
        var round = _roundService.GetById(id);
        return Ok(round);
    }

    [HttpPut]
    [Route("Update")]
    public IActionResult Update(UpdateRoundApiRequest roundApiRequest)
    {
        var round = _mapper.Map<Round>(roundApiRequest);
        _roundService.Update(round);
        
        return Ok(GetRoundById(roundApiRequest.RoundId));
    }

    [HttpGet]
    [Route("History")]
    public IActionResult GetHistory(int roundId)
    {
        var x = _roundService.GetHistory(roundId);
        return Ok(x);
    }
    
}