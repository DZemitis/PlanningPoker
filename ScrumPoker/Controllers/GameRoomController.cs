﻿using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Ask user for desired name of the game room
    /// </summary>
    /// <param name="name">Name for the game room</param>
    /// <returns>Game room with name and ID</returns>
    [HttpPost]
    [Route("Create")]
    public IActionResult CreateGameRoom(string name)
    {
        return Created("", _gameRoomService.Create(name));
    }

    /// <summary>
    /// Update game room
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Updated game room</returns>
    [HttpPut]
    [Route("Update")]
    public IActionResult UpdateGameRoom(string id)
    {
        _gameRoomService.Update(id);

        return Ok(_gameRoomService.GetById(id));
    }

    /// <summary>
    /// Returns full list of game rooms
    /// </summary>
    /// <returns></returns>
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
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetRoomById(string id)
    {
        var gameRoom = _gameRoomService.GetById(id);

        return Ok(gameRoom);
    }

    /// <summary>
    /// Delete all available game rooms
    /// </summary>
    /// <returns></returns>
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
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete/{id}")]
    public IActionResult DeleteGameRoomById(string id)
    {
        _gameRoomService.DeleteById(id);

        return Ok();
    }
}