using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        var roundDto = _context.Rounds.SingleOrDefault(x => x.RoundId == id);
        var roundResponse = _mapper.Map<Round>(roundDto);

        return roundResponse;
    }

    public void SetRoundState(Round round)
    {
        var gameRoomDto = GameRoomIdValidation(round.GameRoomId);
        gameRoomDto.CurrentRound.RoundState = round.RoundState;

        _context.SaveChanges();
    }

    public void Update(Round round)
    {
        var roundDto = _context.Rounds.SingleOrDefault(r => r.RoundId == round.RoundId);
        roundDto.RoundState = round.RoundState;

        _context.SaveChanges();
    }


    public List<VoteRegistration> GetHistory(int roundId)
    {
        var x = _context.Rounds.Select(x => x.Votes).First();
        var voteHistory = _mapper.Map<List<VoteRegistration>>(x);
        
        return voteHistory;
    }
}