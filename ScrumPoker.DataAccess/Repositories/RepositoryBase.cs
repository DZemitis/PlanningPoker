using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Repositories;

public abstract class RepositoryBase
{
    protected readonly IScrumPokerContext Context;
    protected readonly ILogger<RepositoryBase> Logger;
    protected readonly IMapper Mapper;

    protected RepositoryBase(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger)
    {
        Mapper = mapper;
        Context = context;
        Logger = logger;
    }

    protected async Task<GameRoomDto> GetGameRoomById(int gameRoomId)
    {
        var gameRoomDto = await Context.GameRooms
            .Include(gr => gr.GameRoomPlayers)
            .ThenInclude(x => x.Player)
            .Include(x => x.Master)
            .Include(x => x.CurrentRound)
            .Include(x => x.Rounds)
            .SingleOrDefaultAsync(g => g.Id == gameRoomId);

        if (gameRoomDto != null) return gameRoomDto;
        Logger.LogWarning("Game Room with ID {Id} could not been found", gameRoomId);
        throw new IdNotFoundException($"{typeof(GameRoom)} with ID {gameRoomId} not found");
    }

    protected async Task<PlayerDto> GetPlayerById(int playerId)
    {
        var playerDto = await Context.Players
            .Include(p => p.PlayerGameRooms)
            .ThenInclude(x => x.GameRoom)
            .SingleOrDefaultAsync(p => p.Id == playerId);

        if (playerDto != null) return playerDto;
        Logger.LogWarning("Player with ID {Id} could not been found", playerId);
        throw new IdNotFoundException($"{typeof(Player)} with ID {playerId} not found");
    }

    protected async Task<RoundDto> GetRoundById(int roundId)
    {
        var roundDto = await Context.Rounds
            .Include(x => x.Votes)
            .SingleOrDefaultAsync(r => r.RoundId == roundId);

        if (roundDto != null) return roundDto;
        Logger.LogWarning("Round with ID {roundId} could not been found", roundId);
        throw new IdNotFoundException($"{typeof(Round)} with ID {roundId} not found");
    }

    protected async Task<VoteDto> GetVoteById(int voteId)
    {
        var voteDto = await Context.Votes.SingleOrDefaultAsync(x => x.Id == voteId);

        if (voteDto != null) return voteDto;
        Logger.LogWarning("Vote with ID {id} could not be found", voteId);
        throw new IdNotFoundException($"{typeof(Vote)} with ID {voteId} not found");
    }
}