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
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly IMapper _mapper;
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService, IMapper mapper, ILogger<PlayerController> logger)
    {
        _playerService = playerService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    ///     Returns full list of players
    /// </summary>
    /// <returns>List of players</returns>
    [HttpGet]
    [Authorize]
    [Route("List")]
    public async Task<IActionResult> GetAllPlayers()
    {
        _logger.LogInformation("Request to get all players list");
        var playerList = await _playerService.GetAll();
        var playerDisplayList = playerList.Select(player => _mapper.Map<PlayerApiResponse>(player)).ToList();
        return Ok(playerDisplayList);
    }

    /// <summary>
    ///     Returns player by ID
    /// </summary>
    /// <param name="id">ID of the player</param>
    /// <returns>Player by ID</returns>
    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    public async Task<IActionResult> GetPlayerById(int id)
    {
        _logger.LogInformation("Request to get player by ID {id}", id);
        var player = await _playerService.GetById(id);
        var playerDisplay = _mapper.Map<PlayerApiResponse>(player);

        return Ok(playerDisplay);
    }

    /// <summary>
    ///     Ask user for name and email of the player
    /// </summary>
    /// <param name="playerRequest">Player's name and email</param>
    /// <returns>Player name, email and ID</returns>
    [HttpPost]
    [Authorize]
    [Route("Create")]
    public async Task<IActionResult> CreatePlayer(CreatePlayerApiRequest playerRequest)
    {
        _logger.LogInformation("Request to create player with name {name}, email {email}", playerRequest.Name,
            playerRequest.Email);
        var createPlayerRequest = _mapper.Map<Player>(playerRequest);
        var createPlayer = await _playerService.Create(createPlayerRequest);
        var playerResponse = _mapper.Map<PlayerApiResponse>(createPlayer);

        return Created("", playerResponse);
    }

    /// <summary>
    ///     S
    ///     Update player - name and email.
    /// </summary>
    /// <param name="playerRequest">Player with ID</param>
    /// <returns>Updated player</returns>
    [HttpPut]
    [Authorize]
    [Route("Update")]
    public async Task<IActionResult> UpdatePlayer(UpdatePlayerApiRequest playerRequest)
    {
        _logger.LogInformation("Request to change players name to {name}",
            playerRequest.Name);
        var updatePlayerRequest = _mapper.Map<Player>(playerRequest);
        var updatePlayer = await _playerService.Update(updatePlayerRequest);
        var updatePlayerResponse = _mapper.Map<PlayerApiResponse>(updatePlayer);

        return Ok(updatePlayerResponse);
    }

    /// <summary>
    ///     Delete specified player by ID
    /// </summary>
    /// <param name="id">ID of the player</param>
    /// <returns>Confirmation of deletion</returns>
    [HttpDelete]
    [Authorize]
    [Route("Delete/{id:int}")]
    public async Task<IActionResult> DeletePlayerById(int id)
    {
        _logger.LogInformation("Request to delete player(ID {id})", id);
        await _playerService.DeleteById(id);

        return Ok($"Player with ID : {id} has been deleted");
    }
}