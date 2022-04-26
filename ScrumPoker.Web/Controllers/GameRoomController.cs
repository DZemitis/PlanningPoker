using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataBase.Interfaces;
using ScrumPoker.Web.Models.Models.WebRequest;
using ScrumPoker.Web.Models.Models.WebResponse;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GameRoomController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public GameRoomController(IGameRoomService gameRoomService, IMapper mapper, IPlayerService playerService)
    {
        _gameRoomService = gameRoomService;
        _mapper = mapper;
        _playerService = playerService;
    }

    /// <summary>
    /// Ask user for desired name of the game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with name</param>
    /// <returns>Game room with name and ID</returns>
    [HttpPost]
    [Route("Create")]
    public IActionResult CreateGameRoom(CreateGameRoomApiRequest gameRoomRequest)
    {
        var createGameRoomRequest = _mapper.Map<GameRoom>(gameRoomRequest);
        var createGameRoom = _gameRoomService.Create(createGameRoomRequest);
        var gameRoomResponse = _mapper.Map<GameRoomApiResponse>(createGameRoom);

        return Created("", gameRoomResponse);
    }

    /// <summary>S
    /// Update game room, change name for now.
    /// </summary>
    /// <param name="gameRoomRequest">Game room with ID</param>
    /// <returns>Updated game room</returns>
    [HttpPut]
    [Route("Update")]
    public IActionResult UpdateGameRoom(UpdateGameRoomApiRequest gameRoomRequest)
    {
        var updateGameRoomRequest = _mapper.Map<GameRoom>(gameRoomRequest);
        var updateGameRoom = _gameRoomService.Update(updateGameRoomRequest);
        var updateGameRoomResponse = _mapper.Map<GameRoomApiResponse>(updateGameRoom);
        
        return Ok(updateGameRoomResponse);
    }

    /// <summary>
    /// Returns full list of game rooms
    /// </summary>
    /// <returns>List of game rooms</returns>
    [HttpGet]
    [Route("List")]
    public IActionResult GetFullGameRoomList()
    {
        var gameRoomDisplayList = new List<GameRoomApiResponse>();
        var gameRoomList = _gameRoomService.GetAll();
        foreach (var gameRoom in gameRoomList)
        {
            gameRoomDisplayList.Add(_mapper.Map<GameRoomApiResponse>(gameRoom)); 
        }
        return Ok(gameRoomDisplayList);
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
        var gameRoomDisplay = _mapper.Map<GameRoomApiResponse>(gameRoom);

        return Ok(gameRoomDisplay);
    }

    /// <summary>
    /// Delete all available game rooms
    /// </summary>
    /// <returns>Confirmation of deleting all game rooms</returns>
    [HttpDelete]
    [Route("DeleteAll")]
    public IActionResult DeleteAllGameRooms()
    {
        _gameRoomService.DeleteAll();

        return Ok("All game rooms has been deleted");
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

        return Ok($"Game room with ID : {id} has been deleted");
    }

    /// <summary>
    /// Add player to game room
    /// </summary>
    /// <param name="idOfGameRoomToAdd">ID of the game room</param>
    /// <param name="idOfPlayerToAdd">ID of the player</param>
    /// <returns>Updated Game Room</returns>
    [HttpPut]
    [Route("AddPlayer")]
    public IActionResult AddPlayerToRoom(int idOfGameRoomToAdd, int idOfPlayerToAdd)
    {
        _playerService.AddGameRoom(idOfGameRoomToAdd, idOfPlayerToAdd);
        _gameRoomService.AddPlayer(idOfGameRoomToAdd, idOfPlayerToAdd);
        var updatedGameRoom = _mapper.Map<GameRoomApiResponse>(_gameRoomService.GetById(idOfGameRoomToAdd));
        
        return Ok(updatedGameRoom);
    }
}