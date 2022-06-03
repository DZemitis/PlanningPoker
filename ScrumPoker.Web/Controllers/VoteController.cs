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
    public IActionResult GetAllFromRound(int id)
    {
        return Ok();
    }

    [HttpPost]
    [Route("Create")]
    public IActionResult Create(VoteApiRequest voteApiRequest)
    {
        var voteRequest = _mapper.Map<VoteRegistration>(voteApiRequest);
        var voteResponse = _voteRegistrationService.Create(voteRequest);

        return Created("", voteResponse);
    }

    [HttpDelete]
    [Route("ClearVotes")]
    public IActionResult ClearRoundVotes(VoteApiRequest voteApiRequest)
    {
        var voteRequest = _mapper.Map<VoteRegistration>(voteApiRequest);
        _voteRegistrationService.ClearRoundVotes(voteRequest);
        
        return Ok($"All votes from round(ID: {voteApiRequest.RoundId}) has been cleared");
    }
}