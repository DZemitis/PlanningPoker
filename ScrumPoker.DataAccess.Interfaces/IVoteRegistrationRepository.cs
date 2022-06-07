using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IVoteRegistrationRepository
{
    List<VoteRegistration> GetById(int id);
    VoteRegistration Create(VoteRegistration vote);
    void ClearRoundVotes(int roundId);
}