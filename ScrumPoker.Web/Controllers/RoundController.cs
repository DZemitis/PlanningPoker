using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Web.Models.Models.WebRequest;
using ScrumPoker.Web.Models.Models.WebResponse;

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
        var roundResponse = _mapper.Map<RoundApiResponse>(round);
        
        return Ok(roundResponse);
    }

    [HttpPut]
    [Route("SetState")]
    public IActionResult SetState(UpdateRoundApiRequest roundApiRequest)
    {
        var roundRequest = _mapper.Map<Round>(roundApiRequest);
        _roundService.SetState(roundRequest);
        var round = _roundService.GetById(roundRequest.RoundId);
        var roundResponse = _mapper.Map<RoundApiResponse>(round);
        
        return Ok(roundResponse);
    }

    [HttpPut]
    [Route("Update")]
    public IActionResult Update(UpdateDescriptionRoundApiRequest roundApiRequest)
    {
        var roundRequest = _mapper.Map<Round>(roundApiRequest);
        _roundService.Update(roundRequest);
        var round = _roundService.GetById(roundRequest.RoundId);
        var roundResponse = _mapper.Map<RoundApiResponse>(round);
        
        return Ok(roundResponse);
    }
}