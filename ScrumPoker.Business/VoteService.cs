using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;
/// <inheritdoc />
public class VoteService : IVoteService
{
    private readonly IVoteRepository _voteRepository;

    public VoteService(IVoteRepository voteRepository)
    {
        _voteRepository = voteRepository;
    }

    public List<Vote> GetListById(int id)
    {
        return _voteRepository.GetListById(id);
    }

    public Vote GetById(int id)
    {
        return _voteRepository.GetById(id);
    }

    public Vote CreateOrUpdate(Vote vote)
    {
        return _voteRepository.CreateOrUpdate(vote);
    }
    
    public void ClearRoundVotes(int roundId)
    {
        _voteRepository.ClearRoundVotes(roundId);
    }
}