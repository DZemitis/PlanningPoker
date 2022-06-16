using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IGameRoomRepository
{
    /// <summary>
    ///     Returns full list of game rooms
    /// </summary>
    /// <returns>List of all game rooms</returns>
    List<GameRoom> GetAll();

    /// <summary>
    ///     Return game room by ID
    /// </summary>
    /// <param name="id">ID of the game room</param>
    /// <returns>Game room found by ID</returns>
    GameRoom GetById(int id);

    /// <summary>
    ///     Adds game room to data base
    /// </summary>
    /// <param name="gameRoomRequest">Game room with name</param>
    /// <returns>Game room with desired name and generated ID</returns>
    GameRoom Create(GameRoom gameRoomRequest);

    /// <summary>
    ///     Update an game room
    /// </summary>
    /// <param name="gameRoomRequest">Game room with ID</param>
    /// <returns>Updated game room</returns>
    GameRoom Update(GameRoom gameRoomRequest);

    /// <summary>
    ///     Delete all available game rooms
    /// </summary>
    void DeleteAll();

    /// <summary>
    ///     Delete specified game room
    /// </summary>
    /// <param name="id">ID of the game room</param>
    void DeleteById(int id);

    /// <summary>
    ///     Remove player from the game room
    ///     Remove game room from the player
    /// </summary>
    /// <param name="gameRoomId">ID of the game room</param>
    /// <param name="playerId">ID of the player</param>
    void RemovePlayerById(int gameRoomId, int playerId);

    /// <summary>
    ///     Add Player to the room
    /// </summary>
    /// <param name="gameRoomId">ID of the game room</param>
    /// <param name="playerId"> ID of the player</param>
    void AddPlayerToRoom(int gameRoomId, int playerId);
}