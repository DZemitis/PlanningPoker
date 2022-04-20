using ScrumPoker.Core.Models;

namespace ScrumPoker.DataBase.Interfaces;

public interface IGameRoomRepository
{
    /// <summary>
    /// Adds game room to data base
    /// </summary>
    /// <param name="gameRoomRequest">Game room with name</param>
    /// <returns>Game room with desired name and generated ID</returns>
    GameRoom Create(GameRoom gameRoomRequest);
    
    /// <summary>
    /// Returns full list of game rooms
    /// </summary>
    /// <returns></returns>
    List<GameRoom> GetAll();

    /// <summary>
    /// Update an game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with ID</param>
    /// <exception cref="Exception">Not yet implemented</exception>
    GameRoom Update(GameRoom gameRoomRequest);
    
    /// <summary>
    /// Delete all available game rooms
    /// </summary>
    void DeleteAll();
    
    /// <summary>
    /// Delete specified game room
    /// </summary>
    /// <param name="id">ID of the game room</param>
    void DeleteById(int id);
    
    /// <summary>
    /// Return game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns></returns>
    IEnumerable<GameRoom> GetById(int id);
}