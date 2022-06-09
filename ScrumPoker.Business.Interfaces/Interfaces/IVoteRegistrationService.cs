using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IVoteRegistrationService
{
    List<VoteRegistration> GetListById(int id);
    VoteRegistration GetById(int id);
    VoteRegistration Create(VoteRegistration vote);
    void Update(VoteRegistration vote);
    void ClearRoundVotes(int roundId);
}