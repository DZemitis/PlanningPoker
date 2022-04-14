using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Core.Models;
using ScrumPoker.Core.Services;

namespace ScrumPoker.Controllers;
[ApiController]
[Route("[controller]")]
public class GameRoomController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;

    public GameRoomController(IGameRoomService gameRoomService)
    {
        _gameRoomService = gameRoomService;
    }


    [HttpPost]
    [Route("Create")]
    public IActionResult CreateGameRoom(GameRoom name)
    {
        _gameRoomService.CreateGameRoom(name);
        
        return Created("", name);
    }

    [HttpGet]
    [Route("List")]
    public IActionResult GetFullGameRoomList()
    {
        return Ok(_gameRoomService.GetAllGameRooms());
    }

    [HttpGet]
    [Route("{name}")]
    public IActionResult GetRoomByName(string name)
    {
        var gameRoom = _gameRoomService.GetGameRoomByName(name);

        return Ok(gameRoom);
    }

    [HttpDelete]
    [Route("DeleteAll")]
    public IActionResult DeleteAllGameRooms()
    {
        _gameRoomService.DeleteAllGameRooms();
        
        return Ok();
    }  
    
    [HttpDelete]
    [Route("Delete/{name}")]
    public IActionResult DeleteGameRoomByName(string name)
    {
        _gameRoomService.DeleteGameRoomByName(name);
        
        return Ok();
    }
}