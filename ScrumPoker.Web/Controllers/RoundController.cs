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

    /// <summary>
    /// Returns specific round with votes
    /// </summary>
    /// <param name="id">Round ID</param>
    /// <returns>Round</returns>
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetRoundById(int id)
    {
        _logger.LogInformation("Request to get round with ID {Id}", id);
        var round = _roundService.GetById(id);
        var roundResponse = _mapper.Map<RoundApiResponse>(round);
        
        return Ok(roundResponse);
    }

    /// <summary>
    /// Request to set specific state of the round
    /// </summary>
    /// <param name="roundApiRequest">Set round request</param>
    /// <returns>Updated Round</returns>
    [HttpPut]
    [Route("SetState")]
    public IActionResult SetState(UpdateRoundApiRequest roundApiRequest)
    {
        _logger.LogInformation("Request to change round(ID {id}) state", roundApiRequest.RoundId);
        var roundRequest = _mapper.Map<Round>(roundApiRequest);
        _roundService.SetState(roundRequest);
        var round = _roundService.GetById(roundRequest.RoundId);
        var roundResponse = _mapper.Map<RoundApiResponse>(round);
        
        return Ok(roundResponse);
    }

    /// <summary>
    /// Request to update round description
    /// </summary>
    /// <param name="roundApiRequest">Round update request</param>
    /// <returns>Updated Round</returns>
    [HttpPut]
    [Route("Update")]
    public IActionResult Update(UpdateDescriptionRoundApiRequest roundApiRequest)
    {
        _logger.LogInformation("Request to change description for the round (ID {id})", roundApiRequest.RoundId);
        var roundRequest = _mapper.Map<Round>(roundApiRequest);
        _roundService.Update(roundRequest);
        var round = _roundService.GetById(roundRequest.RoundId);
        var roundResponse = _mapper.Map<RoundApiResponse>(round);
        
        return Ok(roundResponse);
    }
}