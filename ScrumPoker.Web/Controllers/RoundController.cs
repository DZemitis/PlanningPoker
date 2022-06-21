using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    private readonly ILogger<RoundController> _logger;
    private readonly IMapper _mapper;
    private readonly IRoundService _roundService;

    public RoundController(IRoundService roundService, IMapper mapper, ILogger<RoundController> logger)
    {
        _roundService = roundService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    ///     Returns specific round with votes
    /// </summary>
    /// <param name="id">Round ID</param>
    /// <returns>Round</returns>
    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRoundById(int id)
    {
        _logger.LogInformation("Request to get round with ID {Id}", id);
        var round = await _roundService.GetById(id);
        var roundResponse = _mapper.Map<RoundApiResponse>(round);

        return Ok(roundResponse);
    }

    /// <summary>
    ///     Creates a round
    /// </summary>
    /// <param name="roundApiRequest">Game room ID and round description</param>
    /// <returns>Created round</returns>
    [HttpPost]
    [Authorize]
    [Route("Create")]
    public async Task<IActionResult> CreateRound(CreateRoundApiRequest roundApiRequest)
    {
        _logger.LogInformation("Request to create a new round");
        var roundRequest = _mapper.Map<Round>(roundApiRequest);
        var roundResponse = await _roundService.Create(roundRequest);

        return Created("", roundResponse);
    }

    /// <summary>
    ///     Request to set specific state of the round
    /// </summary>
    /// <param name="roundApiRequest">Set round request</param>
    /// <returns>Updated Round</returns>
    [HttpPut]
    [Authorize]
    [Route("SetState")]
    public async Task<IActionResult> SetState(UpdateRoundApiRequest roundApiRequest)
    {
        _logger.LogInformation("Request to change round(ID {id}) state", roundApiRequest.RoundId);
        var roundRequest = _mapper.Map<Round>(roundApiRequest);
        await _roundService.SetState(roundRequest);
        var round = await _roundService.GetById(roundRequest.RoundId);
        var roundResponse = _mapper.Map<RoundApiResponse>(round);

        return Ok(roundResponse);
    }

    /// <summary>
    ///     Request to update round description
    /// </summary>
    /// <param name="roundApiRequest">Round update request</param>
    /// <returns>Updated Round</returns>
    [HttpPut]
    [Authorize]
    [Route("Update")]
    public async Task<IActionResult> Update(UpdateDescriptionRoundApiRequest roundApiRequest)
    {
        _logger.LogInformation("Request to change description for the round (ID {id})", roundApiRequest.RoundId);
        var roundRequest = _mapper.Map<Round>(roundApiRequest);
        await _roundService.Update(roundRequest);
        var round = await _roundService.GetById(roundRequest.RoundId);
        var roundResponse = _mapper.Map<RoundApiResponse>(round);

        return Ok(roundResponse);
    }
}