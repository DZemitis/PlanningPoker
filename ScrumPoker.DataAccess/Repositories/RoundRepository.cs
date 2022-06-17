using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Repositories;

/// <inheritdoc cref="ScrumPoker.DataAccess.Interfaces.IRoundRepository" />
public class RoundRepository : RepositoryBase, IRoundRepository
{
    public RoundRepository(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger) : base(mapper,
        context, logger)
    {
    }

    public async Task<Round> GetById(int id)
    {
        var roundDto = await GetRoundById(id);
        var roundResponse = Mapper.Map<Round>(roundDto);

        return roundResponse;
    }

    public async Task<Round> Create(Round roundRequest)
    {
        var createRound = new RoundDto
        {
            Description = roundRequest.Description,
            GameRoomId = roundRequest.GameRoomId,
            RoundState = RoundState.Grooming
        };

        var gameRoomDto = await GetGameRoomById(roundRequest.GameRoomId);

        gameRoomDto.CurrentRound = createRound;
        await Context.Rounds.AddAsync(createRound);
        gameRoomDto.Rounds.Add(createRound);
        await Context.SaveChangesAsync();

        var roundResponse = Mapper.Map<Round>(createRound);
        return roundResponse;
    }

    public async Task SetState(Round roundRequest)
    {
        await GetRoundById(roundRequest.RoundId);
        var roundDto = await Context.Rounds.SingleOrDefaultAsync(r => r.RoundId == roundRequest.RoundId);
        roundDto!.RoundState = roundRequest.RoundState;

        await Context.SaveChangesAsync();
    }

    public async Task Update(Round roundRequest)
    {
        var roundDto = await GetRoundById(roundRequest.RoundId);
        roundDto.Description = roundRequest.Description;

        await Context.SaveChangesAsync();
    }
}