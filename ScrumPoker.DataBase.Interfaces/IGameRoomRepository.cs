using ScrumPoker.Core.Models;

namespace ScrumPoker.DataBase.Interfaces;

public interface IGameRoomRepository
{
    /// <summary>
    /// Adds game room to data base
    /// </summary>
    /// <param name="name">Name of the game room</param>
    /// <returns>Created game room</returns>
    GameRoom Create(string name);
    
    /// <summary>
    /// Returns full list of game rooms
    /// </summary>
    /// <returns></returns>
    List<GameRoom> GetAll();
    
    /// <summary>
    /// Update an game room
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <exception cref="Exception">Not yet implemented</exception>
    void Update(string id);
    
    /// <summary>
    /// Delete all available game rooms
    /// </summary>
    void DeleteAll();
    
    /// <summary>
    /// Delete specified game room
    /// </summary>
    /// <param name="id">ID of the game room</param>
    void DeleteById(string id);
    
    /// <summary>
    /// Return game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns></returns>
    IEnumerable<GameRoom> GetById(string id);
}