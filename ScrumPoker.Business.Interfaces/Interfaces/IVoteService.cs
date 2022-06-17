using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IVoteService
{
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