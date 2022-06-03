using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IVoteRegistrationRepository
{
    VoteRegistration GetById(int id);
    VoteRegistration Create(VoteRegistration vote);
    void ClearRoundVotes(VoteRegistration vote);
}