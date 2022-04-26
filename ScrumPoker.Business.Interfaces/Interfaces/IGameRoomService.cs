using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IGameRoomService
{
    /// <summary>
    /// Create a game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with name</param>
    /// <returns>Created game room</returns>
    GameRoom Create(GameRoom gameRoomRequest);

    /// <summary>
    /// Search game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Game room found by ID</returns>
    GameRoom GetById(int id);

    /// <summary>
    /// Get a list of all game rooms
    /// </summary>
    /// <returns>List of all game rooms</returns>
    List<GameRoom> GetAll();

    /// <summary>
    /// Update an game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with ID</param>
    /// <returns>Updated game room</returns>
    GameRoom Update(GameRoom gameRoomRequest);

    /// <summary>
    /// Deletes all available game rooms
    /// </summary>
    void DeleteAll();

    /// <summary>
    /// Delete specified game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    void DeleteById(int id);

    /// <summary>
    /// Add player to the game room
    /// </summary>
    /// <param name="gameRoom">Game rooms ID</param>
    /// <param name="player">Players ID</param>
    void AddPlayer(int gameRoom, int player);

    /// <summary>
    /// Remove player from the game room
    /// </summary>
    /// <param name="gameRoomId">ID of the game room</param>
    /// <param name="playerId">ID of the player</param>
    void RemovePlayer(int gameRoomId, int playerId);
}