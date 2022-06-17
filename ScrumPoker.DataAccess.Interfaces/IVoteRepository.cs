using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IVoteRepository
{
    /// <summary>
    ///     Returns list of votes in a round
    /// </summary>
    /// <param name="id">ID of the round</param>
    /// <returns>List of votes</returns>
    Task<List<Vote>> GetListById(int id);

    /// <summary>
    ///     Returns a vote with specific ID
    /// </summary>
    /// <param name="id">ID of the vote</param>
    /// <returns>Vote by ID</returns>
    Task<Vote> GetById(int id);

    /// <summary>
    ///     Asks user to create a vote
    /// </summary>
    /// <param name="vote">Vote request</param>
    /// <returns>Created vote with ID</returns>
    Task<Vote> CreateOrUpdate(Vote vote);

    /// <summary>
    ///     Clears all vote in specific round
    /// </summary>
    /// <param name="roundId">Round ID</param>
    Task ClearRoundVotes(int roundId);
}