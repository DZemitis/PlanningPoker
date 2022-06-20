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
    public async Task<IActionResult> GetRoundById(int id)
    {
        var round = await _roundService.GetById(id);
        return Ok(round);
    public async Task<IActionResult> CreateRound(CreateRoundApiRequest roundApiRequest)
        var roundResponse = await _roundService.Create(roundRequest);
    public async Task<IActionResult> SetState(UpdateRoundApiRequest roundApiRequest)
        await _roundService.SetState(roundRequest);
        var round = await _roundService.GetById(roundRequest.RoundId);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Update(UpdateDescriptionRoundApiRequest roundApiRequest)
    {
        var round = _mapper.Map<Round>(roundApiRequest);
        _roundService.Update(round);
        await _roundService.Update(roundRequest);
        var round = await _roundService.GetById(roundRequest.RoundId);

        return Ok(GetRoundById(roundApiRequest.RoundId));
    }
}