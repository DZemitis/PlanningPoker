using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Web.Models.Models.WebRequest;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class VoteController : ControllerBase
{
    private readonly ILogger<VoteController> _logger;
    private readonly IMapper _mapper;
    private readonly IVoteService _voteService;

    public VoteController(IVoteService voteService, IMapper mapper,
        ILogger<VoteController> logger)
    {
        _voteService = voteService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    ///     Returns a vote with specific ID
    /// </summary>
    /// <param name="id">ID of the vote</param>
    /// <returns>Vote by ID</returns>
    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Request to geta a vote with ID {Id}", id);
        var voteResponse = await _voteService.GetById(id);

        return Ok(voteResponse);
    }

    /// <summary>
    ///     Asks user to create/update a vote
    /// </summary>
    /// <param name="voteApiRequest">Vote request with player ID, round ID and vote</param>
    /// <returns>Created/Updated vote with ID</returns>
    [HttpPost]
    [Authorize]
    [Route("Create/Update")]
    public async Task<IActionResult> CreateOrUpdate(VoteApiRequest voteApiRequest)
    {
        _logger.LogInformation("Request to create a vote in round (ID {roundId})",
            voteApiRequest.RoundId);
        var voteRequest = _mapper.Map<Vote>(voteApiRequest);
        var voteResponse = await _voteService.CreateOrUpdate(voteRequest);

        return Created("", voteResponse);
    }


    /// <summary>
    ///     Clears all vote in specific round
    /// </summary>
    /// <param name="roundId">Round ID</param>
    /// <returns>Message, that all votes has been cleared in provided round</returns>
    [HttpDelete]
    [Authorize]
    [Route("ClearVotes")]
    public async Task<IActionResult> ClearRoundVotes(int roundId)
    {
        _logger.LogInformation("Request to clear all votes in Round with ID {roundId}", roundId);
        await _voteService.ClearRoundVotes(roundId);

        return Ok($"All votes from round(ID: {roundId}) has been cleared");
    }
}