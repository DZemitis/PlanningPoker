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

    [HttpGet]
    [Route("GetAllFromRound")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok();
        var voteResponse = await _voteService.GetById(id);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateOrUpdate(VoteApiRequest voteApiRequest)
    {
        var voteRequest = _mapper.Map<VoteRegistration>(voteApiRequest);
        var voteResponse = _voteRegistrationService.Create(voteRequest);
        var voteResponse = await _voteService.CreateOrUpdate(voteRequest);

        return Created("", voteResponse);
    }

    [HttpDelete]
    [Route("ClearVotes")]
    public async Task<IActionResult> ClearRoundVotes(int roundId)
    {
        var voteRequest = _mapper.Map<VoteRegistration>(voteApiRequest);
        await _voteService.ClearRoundVotes(roundId);
        
        return Ok($"All votes from round(ID: {voteApiRequest.RoundId}) has been cleared");
    }
}