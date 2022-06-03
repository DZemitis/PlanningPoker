using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

public class VoteRegistrationService : IVoteRegistrationService
{
    private readonly IVoteRegistrationRepository _voteRegistrationRepository;

    public VoteRegistrationService(IVoteRegistrationRepository voteRegistrationRepository)
    {
        _voteRegistrationRepository = voteRegistrationRepository;
    }

    public VoteRegistration GetById(int id)
    {
        return _voteRegistrationRepository.GetById(id);
    }

    public VoteRegistration Create(VoteRegistration vote)
    {
        return _voteRegistrationRepository.Create(vote);
    }

    public void Update(VoteRegistration vote)
    {
        throw new NotImplementedException();
    }

    public void ClearRoundVotes(VoteRegistration vote)
    {
        _voteRegistrationRepository.ClearRoundVotes(vote);
    }
}