using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Core.Models;
using ScrumPoker.Core.Services;
using ScrumPoker.Web.Models.Models.WebRequest;
using ScrumPoker.Web.Models.Models.WebResponse;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GameRoomController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;
    private readonly IMapper _mapper;

    public GameRoomController(IGameRoomService gameRoomService, IMapper mapper)
    {
        _gameRoomService = gameRoomService;
        _mapper = mapper;
    }

    /// <summary>
    /// Ask user for desired name of the game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with name</param>
    /// <returns>Game room with name and ID</returns>
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateGameRoom(CreateGameRoomRequest gameRoomRequest)
    {
        var createGameRoomRequest = _mapper.Map<GameRoom>(gameRoomRequest);
        var createGameRoom = _gameRoomService.Create(createGameRoomRequest);
        var gameRoomResponse = _mapper.Map<GameRoomResponse>(createGameRoom);
        
        return Created("", gameRoomResponse);
    }

    /// <summary>S
    /// Update game room, change name for now.
    /// </summary>
    /// <param name="gameRoomRequest">Game room with ID</param>
    /// <returns>Updated game room</returns>
    [HttpPut]
    [Route("Update")]
    public IActionResult UpdateGameRoom(GameRoom gameRoomRequest)
    {
        return Ok(_gameRoomService.Update(gameRoomRequest));
    }

    /// <summary>
    /// Returns full list of game rooms
    /// </summary>
    /// <returns>List of game rooms</returns>
    [HttpGet]
    [Route("List")]
    public IActionResult GetFullGameRoomList()
    {
        return Ok(_gameRoomService.GetAll());
    }

    /// <summary>
    /// Returns game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Game room by ID</returns>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetRoomById(int id)
    {
        var gameRoom = _gameRoomService.GetById(id);

        return Ok(gameRoom);
    }

    /// <summary>
    /// Delete all available game rooms
    /// </summary>
    /// <returns>Empty list</returns>
    [HttpDelete]
    [Route("DeleteAll")]
    public IActionResult DeleteAllGameRooms()
    {
        _gameRoomService.DeleteAll();

        return Ok();
    }

    /// <summary>
    /// Delete specified game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Confirmation of deletion</returns>
    [HttpDelete]
    [Route("Delete/{id}")]
    public IActionResult DeleteGameRoomById(int id)
    {
        _gameRoomService.DeleteById(id);

        return Ok();
    }
}