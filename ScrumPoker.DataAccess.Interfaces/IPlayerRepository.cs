using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IPlayerRepository
{
    /// <summary>
    /// Get a list of all playersInGameRoom
    /// </summary>
    /// <returns>List of all players in game room</returns>
    IEnumerable<Player> GetAll();

    /// <summary>
    /// Search player by ID
    /// </summary>
    /// <param name="id">ID of the player</param>
    /// <returns>Player found by ID</returns>
    Player GetById(int id);

    /// <summary>
    /// Create a Player
    /// </summary>
    /// <param name="createPlayerRequest">Player with name and Email</param>
    /// <returns>Created Player</returns>
    Player Create(Player createPlayerRequest);

    /// <summary>
    /// Update an Player
    /// </summary>
    /// <param name="updatePlayerRequest">Player with ID</param>
    /// <returns>Updated player</returns>
    Player Update(Player updatePlayerRequest);
    
    /// <summary>
    /// Delete specified player by ID
    /// </summary>
    /// <param name="id">ID of the player</param>
    void DeleteById(int id);
}