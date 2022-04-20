using ScrumPoker.Core.Models;

namespace ScrumPoker.Core.Services;

public interface IGameRoomService
{
    /// <summary>
    /// Create a game room
    /// </summary>
    /// <param name="name">name of the game room</param>
    /// <returns>Created game room</returns>
    GameRoom Create(string name);

    /// <summary>
    /// Search game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns></returns>
    IEnumerable<GameRoom> GetById(string id);
    
    /// <summary>
    /// Get a list of all game rooms
    /// </summary>
    /// <returns></returns>
    List<GameRoom> GetAll();
    
    /// <summary>
    /// Update an game room
    /// </summary>
    /// <param name="id">ID of the game room</param>
    void Update(string id);
    
    /// <summary>
    /// Deletes all available game rooms
    /// </summary>
    void DeleteAll();
    
    /// <summary>
    /// Delete specified game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    void DeleteById(string id);
}