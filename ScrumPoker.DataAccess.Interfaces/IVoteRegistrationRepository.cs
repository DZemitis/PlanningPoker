using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IVoteRegistrationRepository
{
    List<VoteRegistration> GetListById(int id);
    VoteRegistration GetById(int id);
    VoteRegistration Create(VoteRegistration vote);
    void ClearRoundVotes(int roundId);
    void Update(VoteRegistration vote);
}