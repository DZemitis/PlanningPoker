using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

public class RoundRepository : RepositoryBase ,IRoundRepository
{
    public RoundRepository(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger) : base(mapper, context, logger)
    {
    }

    public Round GetById(int id)
    {
        var roundDto = RoundIdValidation(id);
        var roundResponse = _mapper.Map<Round>(roundDto);

        return roundResponse;
    }

    public void SetRoundState(Round round)
    {
        var gameRoomDto = GameRoomIdValidation(round.GameRoomId);
        gameRoomDto.CurrentRound!.RoundState = round.RoundState;

        _context.SaveChanges();
    }

    public void Update(Round round)
    {
        GameRoomIdValidation(round.GameRoomId);
        var roundDto = _context.Rounds.SingleOrDefault(r => r.RoundId == round.RoundId);
        roundDto!.RoundState = round.RoundState;

        _context.SaveChanges();
    }


    public List<VoteRegistration> GetHistory(int roundId)
    {
        var voteHistory = new List<VoteRegistrationDto>();
        var voteHistoryList = _context.Rounds.Select(x => x.Votes);
        foreach (var votingList in voteHistoryList)
        {
            voteHistory.AddRange(votingList.Where(vote => vote.Id == roundId));
        }

        var voteHistoryResponse = _mapper.Map<List<VoteRegistration>>(voteHistory);
        
        return voteHistoryResponse;
    }
}