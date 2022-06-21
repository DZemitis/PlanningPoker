using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Web.Models.Models.WebRequest;
using ScrumPoker.Web.Models.Models.WebResponse;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GameRoomController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;
    private readonly ILogger<GameRoomController> _logger;
    private readonly IMapper _mapper;

    public GameRoomController(IGameRoomService gameRoomService, IMapper mapper, ILogger<GameRoomController> logger)
    {
        _gameRoomService = gameRoomService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    ///     Returns full list of game rooms
    /// </summary>
    /// <returns>List of game rooms</returns>
    [HttpGet]
    [Authorize]
    [Route("List")]
    public async Task<IActionResult> GetFullGameRoomList()
    {
        _logger.LogInformation("Request to get list of all game rooms");
        var gameRoomList = await _gameRoomService.GetAll();
        var gameRoomListResponse = _mapper.Map<List<GameRoomAllApiResponse>>(gameRoomList);

        return Ok(gameRoomListResponse);
    }

    /// <summary>
    ///     Returns game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Game room by ID</returns>
    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        _logger.LogInformation("Request to find game room with ID {Id}", id);
        var gameRoom = await _gameRoomService.GetById(id);
        var gameRoomResponse = _mapper.Map<GameRoomApiResponse>(gameRoom);

        return Ok(gameRoomResponse);
    }

    /// <summary>
    ///     Ask user for desired name of the game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with name</param>
    /// <returns>Game room with name and ID</returns>
    [HttpPost]
    [Authorize]
    [Route("Create")]
    public async Task<IActionResult> CreateGameRoom(CreateGameRoomApiRequest gameRoomRequest)
    {
        _logger.LogInformation("Request to create game room with name - {Name}", gameRoomRequest.Name);
        var createGameRoomRequest = _mapper.Map<GameRoom>(gameRoomRequest);

        var createGameRoom = await _gameRoomService.Create(createGameRoomRequest);
        var gameRoomResponse = _mapper.Map<GameRoomAllApiResponse>(createGameRoom);

        return Created("", gameRoomResponse);
    }

    /// <summary>
    ///     Update game room, change name for now.
    /// </summary>
    /// <param name="gameRoomRequest">Game room with ID</param>
    /// <returns>Updated game room</returns>
    [HttpPut]
    [Authorize]
    [Route("Update")]
    public async Task<IActionResult> UpdateGameRoom(UpdateGameRoomApiRequest gameRoomRequest)
    {
        _logger.LogInformation("Request to change game rooms(ID {Id}) name to - {Name}", gameRoomRequest.Id,
            gameRoomRequest.Name);
        var updateGameRoomRequest = _mapper.Map<GameRoom>(gameRoomRequest);

        var updateGameRoom = await _gameRoomService.Update(updateGameRoomRequest);
        var updateGameRoomResponse = _mapper.Map<GameRoomApiResponse>(updateGameRoom);

        return Ok(updateGameRoomResponse);
    }

    /// <summary>
    ///     Delete all available game rooms
    /// </summary>
    /// <returns>Confirmation of deleting all game rooms</returns>
    [HttpDelete]
    [Route("DeleteAll")]
    public async Task<IActionResult> DeleteAllGameRooms()
    {
        _logger.LogInformation("Request to delete all game rooms");
        await _gameRoomService.DeleteAll();

        return Ok("All game rooms has been deleted");
    }

    /// <summary>
    ///     Delete specified game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Confirmation of deletion</returns>
    [HttpDelete]
    [Authorize]
    [Route("Delete/{id:int}")]
    public async Task<IActionResult> DeleteGameRoomById(int id)
    {
        _logger.LogInformation("Request to delete game room with ID - {Id}", id);
        await _gameRoomService.DeleteById(id);

        return Ok($"Game room with ID : {id} has been deleted");
    }

    /// <summary>
    ///     Add player to game room
    /// </summary>
    /// <param name="idOfGameRoomToAdd">ID of the game room</param>
    /// <param name="idOfPlayerToAdd">ID of the player</param>
    /// <returns>Updated Game Room</returns>
    [HttpPut]
    [Authorize]
    [Route("AddPlayer")]
    public async Task<IActionResult> AddPlayerToRoom(int idOfGameRoomToAdd, int idOfPlayerToAdd)
    {
        _logger.LogInformation("Request to add player(ID {PlayerId}) to game room(ID {GameRoomId})", idOfPlayerToAdd,
            idOfGameRoomToAdd);
        await _gameRoomService.AddPlayer(idOfGameRoomToAdd, idOfPlayerToAdd);
        var getGameRoom = await _gameRoomService.GetById(idOfGameRoomToAdd);
        var gameRoomResponse = _mapper.Map<GameRoomAddPlayerApiResponse>(getGameRoom);

        return Ok(gameRoomResponse);
    }

    /// <summary>
    ///     Remove player from the game room
    /// </summary>
    /// <param name="gameRoomId">ID of the game room</param>
    /// <param name="playerId"> ID of the player</param>
    /// <returns>Confirmation of removal</returns>
    [HttpDelete]
    [Authorize]
    [Route("RemovePlayer")]
    public async Task<IActionResult> RemovePlayerFromRoom(int gameRoomId, int playerId)
    {
        _logger.LogInformation("Request to remove player(ID {PlayerId}) from game room(ID {GameRoomId})", playerId,
            gameRoomId);
        await _gameRoomService.RemovePlayer(gameRoomId, playerId);

        return Ok("Player has been removed from room");
    }
}