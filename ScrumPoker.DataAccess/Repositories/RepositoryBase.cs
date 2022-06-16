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
    protected readonly IMapper Mapper;
    protected readonly IScrumPokerContext Context;
    protected readonly ILogger<RepositoryBase> Logger;

    protected RepositoryBase(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger)
    {
        Mapper = mapper;
        Context = context;
        Logger = logger;
    }
    protected GameRoomDto GetGameRoomById(int gameRoomId)
    {
        var gameRoomDto = Context.GameRooms
            .Include(gr=>gr.GameRoomPlayers)
            .ThenInclude(x=>x.Player)
            .Include(x=>x.Master)
            .Include(x=>x.CurrentRound)
            .Include(x=>x.Rounds)
            .SingleOrDefault(g => g.Id == gameRoomId);
        
        if (gameRoomDto != null) return gameRoomDto;
        Logger.LogWarning("Game Room with ID {Id} could not been found", gameRoomId);
        throw new IdNotFoundException($"{typeof(GameRoom)} with ID {gameRoomId} not found");
    }

    protected PlayerDto GetPlayerById(int playerId)
    {
        var playerDto = Context.Players
            .Include(p=>p.PlayerGameRooms).ThenInclude(x=>x.GameRoom)
            .SingleOrDefault(p => p.Id == playerId);

        if (playerDto != null) return playerDto;
        Logger.LogWarning("Player with ID {Id} could not been found", playerId);
        throw new IdNotFoundException($"{typeof(Player)} with ID {playerId} not found");
    }

    protected RoundDto GetRoundById(int roundId)
    {
        var roundDto = Context.Rounds
            .Include(x=>x.Votes)
            .SingleOrDefault(r => r.RoundId == roundId);

        if (roundDto != null) return roundDto;
        Logger.LogWarning("Round with ID {roundId} could not been found", roundId);
        throw new IdNotFoundException($"{typeof(Round)} with ID {roundId} not found");
    }

    protected VoteDto GetVoteById(int voteId)
    {
        var voteDto = Context.Votes.SingleOrDefault(x => x.Id == voteId);
        
        if (voteDto != null) return voteDto;
        Logger.LogWarning("Vote with ID {id} could not be found", voteId);
        throw new IdNotFoundException($"{typeof(Vote)} with ID {voteId} not found");
    }
}