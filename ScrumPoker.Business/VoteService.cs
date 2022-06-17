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

    public async Task<Vote> GetById(int id)
    {
        return await _voteRepository.GetById(id);
    }

    public async Task<Vote> CreateOrUpdate(Vote vote)
    {
        return await _voteRepository.CreateOrUpdate(vote);
    }

    public async Task ClearRoundVotes(int roundId)
    {
        await _voteRepository.ClearRoundVotes(roundId);
    }
}