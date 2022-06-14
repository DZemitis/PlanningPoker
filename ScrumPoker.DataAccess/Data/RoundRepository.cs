using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;
/// <inheritdoc cref="ScrumPoker.DataAccess.Interfaces.IRoundRepository" />
public class RoundRepository : RepositoryBase ,IRoundRepository
{
    public RoundRepository(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger) : base(mapper, context, logger)
    {
    }

    public Round GetById(int id)
    {
        var roundDto = RoundIdValidation(id);
        var roundResponse = Mapper.Map<Round>(roundDto);

        return roundResponse;
    }

    public Round Create(Round roundRequest)
    {
        var gameRoomDto = GameRoomIdValidation(roundRequest.GameRoomId);
        
        var createRound = new RoundDto
        {
            Description = roundRequest.Description,
            GameRoomId = roundRequest.GameRoomId,
            RoundState = RoundState.Grooming
        };
        
        gameRoomDto.CurrentRound = createRound;
        Context.Rounds.Add(createRound);
        gameRoomDto.Rounds.Add(createRound);
        Context.SaveChanges();

        var roundResponse = Mapper.Map<Round>(createRound);
        return roundResponse;
    }
    
    public void SetState(Round roundRequest)
    {
        RoundIdValidation(roundRequest.RoundId);
        var roundDto = Context.Rounds.SingleOrDefault(r => r.RoundId == roundRequest.RoundId);
        roundDto!.RoundState = roundRequest.RoundState;

        Context.SaveChanges();
    }
    
    public void Update(Round roundRequest)
    {
        RoundIdValidation(roundRequest.RoundId);
        var roundDto = Context.Rounds.SingleOrDefault(x => x.RoundId == roundRequest.RoundId);
        roundDto!.Description = roundRequest.Description;

        Context.SaveChanges();
    }

    public List<VoteRegistration> GetHistory(int roundId)
    {
        var voteHistory = new List<VoteRegistrationDto>();
        var voteHistoryList = Context.Rounds.Include(x=>x.Votes).Select(x=>x.Votes);
        foreach (var votingList in voteHistoryList)
        {
            voteHistory.AddRange(votingList.Where(vote => vote.Id == roundId));
        }

        var voteHistoryResponse = Mapper.Map<List<VoteRegistration>>(voteHistory);
        
        return voteHistoryResponse;
    }
}