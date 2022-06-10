using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Web.Models.Models.WebRequest;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class VoteController : ControllerBase
{
    private readonly IVoteRegistrationService _voteRegistrationService;
    private readonly IMapper _mapper;
    private readonly ILogger<VoteController> _logger;

    public VoteController(IVoteRegistrationService voteRegistrationService, IMapper mapper,
        ILogger<VoteController> logger)
    {
        _voteRegistrationService = voteRegistrationService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Returns list of votes in a round
    /// </summary>
    /// <param name="id">ID of the round</param>
    /// <returns>List of votes</returns>
    [HttpGet]
    [Route("GetAllFromRound")]
    public IActionResult GetAllFromRound(int id)
    {
        _logger.LogInformation("Request to get all votes from a round(ID {Id})", id);
        var voteList = _voteRegistrationService.GetListById(id);
        
        return Ok(voteList);
    }

    /// <summary>
    /// Returns a vote with specific ID
    /// </summary>
    /// <param name="id">ID of the vote</param>
    /// <returns>Vote by ID</returns>
    [HttpGet]
    [Route("{Id:int}")]
    public IActionResult GetById(int id)
    {
        _logger.LogInformation("Request to geta a vote with ID {Id}", id);
        var voteResponse = _voteRegistrationService.GetById(id);

        return Ok(voteResponse);
    }

    /// <summary>
    /// Asks user to create a vote
    /// </summary>
    /// <param name="voteApiRequest">Vote request with player ID, round ID and vote</param>
    /// <returns>Created vote with ID</returns>
    [HttpPost]
    [Route("Create")]
    public IActionResult Create(VoteApiRequest voteApiRequest)
    {
        _logger.LogInformation("Request to create a vote for player with ID {playerId} in round with ID {roundId}", voteApiRequest.PlayerId, voteApiRequest.RoundId);
        var voteRequest = _mapper.Map<VoteRegistration>(voteApiRequest);
        var voteResponse = _voteRegistrationService.Create(voteRequest);

        return Created("", voteResponse);
    }

    /// <summary>
    /// Update a vote, change voting result
    /// </summary>
    /// <param name="voteApiRequest">Vote update request with vote ID, voting result</param>
    /// <returns>Updated vote</returns>
    [HttpPut]
    [Route("Update")]
    public IActionResult Update(UpdateVoteApiRequest voteApiRequest)
    {
        _logger.LogInformation("Request to change vote to {voteResult} (ID {voteId})",voteApiRequest.Vote,voteApiRequest.Id);
        var voteRequest = _mapper.Map<VoteRegistration>(voteApiRequest);
        _voteRegistrationService.Update(voteRequest);
        var voteResponse = _voteRegistrationService.GetById(voteApiRequest.Id);
        
        return Ok(voteResponse);
    }

    /// <summary>
    /// Clears all vote in specific round
    /// </summary>
    /// <param name="roundId">Round ID</param>
    /// <returns>Message, that all votes has been cleared in provided round</returns>
    [HttpDelete]
    [Route("ClearVotes")]
    public IActionResult ClearRoundVotes(int roundId)
    {
        _logger.LogInformation("Request to clear all votes in Round with ID {roundId}", roundId);
        _voteRegistrationService.ClearRoundVotes(roundId);

        return Ok($"All votes from round(ID: {roundId}) has been cleared");
    }
}