using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Interfaces.Interfaces;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthTestController : ControllerBase
{
    private readonly IUserManager _manager;

    public AuthTestController(IUserManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    [Route("CreateToken/{id:int}")]
    public IActionResult CreateToken(int id)
    {
       var jwt= _manager.CreateToken(id);
       
       return Ok(jwt);
    }

    [HttpGet]
    [Authorize]
    [Route("CheckToken")]
    public IActionResult CheckAuth()
    {
        var userId = _manager.GetCurrentUserId();
        return Ok(userId);
    }
}