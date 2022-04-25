using AutoMapper;
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
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public PlayerController(IPlayerService playerService, IMapper mapper)
    {
        _playerService = playerService;
        _mapper = mapper;
    }
    
    
    /// <summary>
    /// Ask user for name and email of the player
    /// </summary>
    /// <param name="playerRequest">Player's name and email</param>
    /// <returns>Player name, email and ID</returns>
    [HttpPost]
    [Route("Create")]
    public IActionResult CreateGameRoom(CreatePlayerApiRequest playerRequest)
    {
        var createPlayerRequest = _mapper.Map<Player>(playerRequest);
        var createPlayer = _playerService.Create(createPlayerRequest);
        var playerResponse = _mapper.Map<PlayerApiResponse>(createPlayer);

        return Created("", playerResponse);
    }

    /// <summary>S
    /// Update player - name and email.
    /// </summary>
    /// <param name="playerRequest">Player with ID</param>
    /// <returns>Updated player</returns>
    [HttpPut]
    [Route("Update")]
    public IActionResult UpdatePlayer(UpdatePlayerApiRequest playerRequest)
    {
        var updatePlayerRequest = _mapper.Map<Player>(playerRequest);
        var updatePlayer = _playerService.Update(updatePlayerRequest);
        var updatePlayerResponse = _mapper.Map<PlayerApiResponse>(updatePlayer);
        
        return Ok(updatePlayerResponse);
    }

    /// <summary>
    /// Returns full list of players
    /// </summary>
    /// <returns>List of players</returns>
    [HttpGet]
    [Route("List")]
    public IActionResult GetAllPlayers()
    {
        return Ok(_playerService.GetAll());
    }

    /// <summary>
    /// Returns player by ID
    /// </summary>
    /// <param name="id">ID of the player</param>
    /// <returns>Player by ID</returns>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetPlayerById(int id)
    {
        var player = _playerService.GetById(id);

        return Ok(player);
    }

    /// <summary>
    /// Delete specified player by ID
    /// </summary>
    /// <param name="id">ID of the player</param>
    /// <returns>Confirmation of deletion</returns>
    [HttpDelete]
    [Route("Delete/{id}")]
    public IActionResult DeletePlayerById(int id)
    {
        _playerService.DeleteById(id);

        return Ok($"Player with ID : {id} has been deleted");
    }
}