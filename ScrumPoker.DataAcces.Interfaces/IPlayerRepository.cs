using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataBase.Interfaces;

public interface IPlayerRepository
{
    /// <summary>
    /// Create a Player
    /// </summary>
    /// <param name="createPlayerRequest">Player with name and Email</param>
    /// <returns>Created Player</returns>
    Player Create(Player createPlayerRequest);

    /// <summary>
    /// Search player by ID
    /// </summary>
    /// <param name="id">ID of the player</param>
    /// <returns>Player found by ID</returns>
    Player GetById(int id);

    /// <summary>
    /// Update an Player
    /// </summary>
    /// <param name="updatePlayerRequest">Player with ID</param>
    /// <returns>Updated player</returns>
    Player Update(Player updatePlayerRequest);

    /// <summary>
    /// Get a list of all playersInGameRoom
    /// </summary>
    /// <returns>List of all players in game room</returns>
    List<Player> GetAll();

    /// <summary>
    /// Delete specified player by ID
    /// </summary>
    /// <param name="id">ID of the player</param>
    void DeleteById(int id);

    /// <summary>
    /// Add game room to the player model
    /// </summary>
    /// <param name="playerToUpdate">Player to update</param>
    void UpdateGameRoomList(Player playerToUpdate);
}