using ScrumPoker.Core.Models;

namespace ScrumPoker.Core.Services;

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
    /// <returns></returns>
    GameRoom GetById(int id);
    
    /// <summary>
    /// Get a list of all game rooms
    /// </summary>
    /// <returns></returns>
    List<GameRoom> GetAll();

    /// <summary>
    /// Update an game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with ID</param>
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
}