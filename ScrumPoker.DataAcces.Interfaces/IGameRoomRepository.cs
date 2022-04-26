using ScrumPoker.Business.Models.Models;

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
    /// <returns>List of all game rooms</returns>
    List<GameRoom> GetAll();

    /// <summary>
    /// Update an game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with ID</param>
    /// <returns>Updated game room</returns>
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
    /// <returns>Game room found by ID</returns>
    GameRoom GetById(int id);

    /// <summary>
    /// Update players list in game room
    /// </summary>
    /// <param name="gameRoomUpdateRequest">Game room to update</param>
    void UpdatePlayerList(GameRoom gameRoomUpdateRequest);
}