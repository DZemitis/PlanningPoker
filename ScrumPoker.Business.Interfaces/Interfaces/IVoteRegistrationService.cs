using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IVoteRegistrationService
{
    VoteRegistration GetById(int id);
    VoteRegistration Create(VoteRegistration vote);
    void Update(VoteRegistration vote);
}