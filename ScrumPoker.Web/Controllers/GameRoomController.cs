using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Web.Models.Models.WebRequest;
using ScrumPoker.Web.Models.Models.WebResponse;
using Serilog;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GameRoomController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;
    private readonly IMapper _mapper;
    private readonly ILogger<GameRoomController> _logger;

    public GameRoomController(IGameRoomService gameRoomService, IMapper mapper, ILogger<GameRoomController> logger)
    {
        _gameRoomService = gameRoomService;
        _mapper = mapper;
        _logger = logger;
    }
    
    /// <summary>
    /// Returns full list of game rooms
    /// </summary>
    /// <returns>List of game rooms</returns>
    [HttpGet]
    [Route("List")]
    public IActionResult GetFullGameRoomList()
    {
        var gameRoomList = _gameRoomService.GetAll();
        var gameRoomListResponse = _mapper.Map<List<GameRoomApiResponse>>(gameRoomList);

        return Ok(gameRoomListResponse);
    }

    /// <summary>
    /// Returns game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Game room by ID</returns>
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetRoomById(int id)
    {
        _logger.LogInformation("User made request to find Game Room with ID - {0}", id);
        var gameRoom = _gameRoomService.GetById(id);
        var gameRoomResponse = _mapper.Map<GameRoomApiResponse>(gameRoom);

        _logger.LogInformation("Game room with ID {0} was returned", gameRoomResponse.Id);
        return Ok(gameRoomResponse);
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
        _logger.LogInformation("User made request to create a game room with {0} as name", gameRoomRequest.Name);
        var createGameRoomRequest = _mapper.Map<GameRoom>(gameRoomRequest);

        var createGameRoom = _gameRoomService.Create(createGameRoomRequest);
        var gameRoomResponse = _mapper.Map<GameRoomApiResponse>(createGameRoom);

        _logger.LogInformation("Game Room with ID {0} was created", gameRoomResponse.Id);
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
        _logger.LogInformation("User made a request to change game room name, game room id - {0} change name to {1}", gameRoomRequest.Id, gameRoomRequest.Name);
        var updateGameRoomRequest = _mapper.Map<GameRoom>(gameRoomRequest);

        var updateGameRoom = _gameRoomService.Update(updateGameRoomRequest);
        var updateGameRoomResponse = _mapper.Map<GameRoomApiResponse>(updateGameRoom);
        
        _logger.LogInformation("Game Room with ID {0} was updated", gameRoomRequest.Id);
        return Ok(updateGameRoomResponse);
    }

    /// <summary>
    /// Delete all available game rooms
    /// </summary>
    /// <returns>Confirmation of deleting all game rooms</returns>
    [HttpDelete]
    [Route("DeleteAll")]
    public IActionResult DeleteAllGameRooms()
    {
        _logger.LogInformation("User made request to delete all game Rooms");
        _gameRoomService.DeleteAll();
        
        _logger.LogInformation("All game rooms has been deleted");
        return Ok("All game rooms has been deleted");
    }

    /// <summary>
    /// Delete specified game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Confirmation of deletion</returns>
    [HttpDelete]
    [Route("Delete/{id:int}")]
    public IActionResult DeleteGameRoomById(int id)
    {
        _logger.LogInformation("User made a request to delete game room with ID {0}", id);
        _gameRoomService.DeleteById(id);

        _logger.LogInformation("Game Room with ID {0} was deleted", id);
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
        _logger.LogInformation("Request to add player with ID {0}, to game room with ID {1} was made", idOfPlayerToAdd, idOfGameRoomToAdd);
        _gameRoomService.AddPlayer(idOfGameRoomToAdd, idOfPlayerToAdd);
        var getGameRoom = _gameRoomService.GetById(idOfGameRoomToAdd);
        var gameRoomResponse = _mapper.Map<GameRoomApiResponse>(getGameRoom);
        
        _logger.LogInformation("Played with ID {0} was added to game room with ID {1}", idOfPlayerToAdd, idOfGameRoomToAdd);
        return Ok(gameRoomResponse);
    }

    /// <summary>
    /// Remove player from the game room
    /// </summary>
    /// <param name="gameRoomId">ID of the game room</param>
    /// <param name="playerId"> ID of the player</param>
    /// <returns>Confirmation of removal</returns>
    [HttpDelete]
    [Route("RemovePlayer")]
    public IActionResult RemovePlayerFromRoom(int gameRoomId, int playerId)
    {
        _logger.LogInformation("User made a request to remove player with ID{0} from game room with id {1}", playerId, gameRoomId);
        _gameRoomService.RemovePlayer(gameRoomId, playerId);

        _logger.LogInformation("Player(ID {0}) got removed from game room(ID {1})", playerId, gameRoomId);
        return Ok("Player has been removed from room");
    }
}