using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IVoteRegistrationRepository
{
    /// <summary>
    /// Returns list of votes in a round
    /// </summary>
    /// <param name="id">ID of the round</param>
    /// <returns>List of votes</returns>
    List<VoteRegistration> GetListById(int id);
    /// <summary>
    /// Returns a vote with specific ID
    /// </summary>
    /// <param name="id">ID of the vote</param>
    /// <returns>Vote by ID</returns>
    VoteRegistration GetById(int id);
    /// <summary>
    /// Asks user to create a vote
    /// </summary>
    /// <param name="vote">Vote request</param>
    /// <returns>Created vote with ID</returns>
    VoteRegistration Create(VoteRegistration vote);
    /// <summary>
    /// Update a vote, change voting result
    /// </summary>
    /// <param name="vote">Vote update request</param>
    void Update(VoteRegistration vote);
    /// <summary>
    /// Clears all vote in specific round
    /// </summary>
    /// <param name="roundId">Round ID</param>
    void ClearRoundVotes(int roundId);
}