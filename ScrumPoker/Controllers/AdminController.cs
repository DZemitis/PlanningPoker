using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Core.Models;
using ScrumPoker.Core.Services;

namespace ScrumPoker.Controllers;
[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;

    public AdminController(IGameRoomService gameRoomService)
    {
        _gameRoomService = gameRoomService;
    }


    [HttpPut]
    [Route("CreateGameRoom")]
    public IActionResult CreateGameRoom(GameRoom name)
    {
        _gameRoomService.CreateGameRoom(name);
        
        return Created("", name);
    }

    [HttpGet]
    [Route("GameRoom/list")]
    public IActionResult GetFullGameRoomList()
    {
        return Ok(_gameRoomService.GetAllGameRooms());
    }

    [HttpGet]
    [Route("GameRoom/{name}")]
    public IActionResult GetRoomByName(string name)
    {
        var gameRoom = _gameRoomService.GetGameRoomByName(name);

        return Ok(gameRoom);
    }

    [HttpDelete]
    [Route("GameRoom/ClearAll")]
    public IActionResult ClearAllGameRooms()
    {
        _gameRoomService.DeleteAllGameRooms();
        
        return Ok();
    }  
    
    [HttpDelete]
    [Route("GameRoom/{name}")]
    public IActionResult RemoveGameRoomByName(string name)
    {
        _gameRoomService.DeleteGameRoomByName(name);
        
        return Ok();
    }
}