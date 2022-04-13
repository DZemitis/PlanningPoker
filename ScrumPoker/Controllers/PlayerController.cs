using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Core.Models;
using ScrumPoker.Logic;

namespace ScrumPoker.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    [HttpPost]
    [Route("Vote")]
    public IActionResult GetVoteDetails(Player details)
    {
        GameLogic.AddPlayerData(details);
        
        return Created("", details);
    }

    [HttpGet]
    [Route("GetResult")]
    public IActionResult GetResult()
    {
        return Ok(GameLogic.SetResult());
    }

    [HttpDelete]
    [Route("ClearVoting")]
    public IActionResult RestartVoting()
    {
        GameLogic.RestartVoting();
        
        return Ok();
    }
}