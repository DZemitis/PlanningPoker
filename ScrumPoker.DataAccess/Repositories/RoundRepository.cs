using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Repositories;

/// <inheritdoc cref="ScrumPoker.DataAccess.Interfaces.IRoundRepository" />
public class RoundRepository : RepositoryBase ,IRoundRepository
{
    public RoundRepository(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger) : base(mapper, context, logger)
    {
    }

    public Round GetById(int id)
    {
        var roundDto = GetRoundById(id);
        var roundResponse = Mapper.Map<Round>(roundDto);

        return roundResponse;
    }

    public Round Create(Round roundRequest)
    {
        var createRound = new RoundDto
        {
            Description = roundRequest.Description,
            GameRoomId = roundRequest.GameRoomId,
            RoundState = RoundState.Grooming
        };
        
        var gameRoomDto = GetGameRoomById(roundRequest.GameRoomId);
        
        gameRoomDto.CurrentRound = createRound;
        Context.Rounds.Add(createRound);
        gameRoomDto.Rounds.Add(createRound);
        Context.SaveChanges();

        var roundResponse = Mapper.Map<Round>(createRound);
        return roundResponse;
    }
    
    public void SetState(Round roundRequest)
    {
        GetRoundById(roundRequest.RoundId);
        var roundDto = Context.Rounds.SingleOrDefault(r => r.RoundId == roundRequest.RoundId);
        roundDto!.RoundState = roundRequest.RoundState;

        Context.SaveChanges();
    }
    
    public void Update(Round roundRequest)
    {
        var roundDto = GetRoundById(roundRequest.RoundId);
        roundDto.Description = roundRequest.Description;

        Context.SaveChanges();
    }
}