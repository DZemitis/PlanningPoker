using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;
/// <inheritdoc />
public class VoteRegistrationService : IVoteRegistrationService
{
    private readonly IVoteRegistrationRepository _voteRegistrationRepository;

    public VoteRegistrationService(IVoteRegistrationRepository voteRegistrationRepository)
    {
        _voteRegistrationRepository = voteRegistrationRepository;
    }

    public List<VoteRegistration> GetListById(int id)
    {
        return _voteRegistrationRepository.GetListById(id);
    }

    public VoteRegistration GetById(int id)
    {
        return _voteRegistrationRepository.GetById(id);
    }

    public VoteRegistration CreateOrUpdate(VoteRegistration vote)
    {
        return _voteRegistrationRepository.CreateOrUpdate(vote);
    }
    
    public void ClearRoundVotes(int roundId)
    {
        _voteRegistrationRepository.ClearRoundVotes(roundId);
    }
}