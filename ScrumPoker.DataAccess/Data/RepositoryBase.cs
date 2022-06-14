using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

public abstract class RepositoryBase
{
    protected readonly IMapper Mapper;
    protected readonly IScrumPokerContext Context;
    private readonly ILogger<RepositoryBase> Logger;

    protected RepositoryBase(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger)
    {
        Mapper = mapper;
        Context = context;
        Logger = logger;
    }
    protected GameRoomDto GameRoomIdValidation(int gameRoomId)
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

    protected PlayerDto PlayerIdValidation(int playerId)
    {
        var playerDto = Context.Players
            .Include(p=>p.PlayerGameRooms)
            .SingleOrDefault(p => p.Id == playerId);

        if (playerDto != null) return playerDto;
        Logger.LogWarning("Player with ID {Id} could not been found", playerId);
        throw new IdNotFoundException($"{typeof(Player)} with ID {playerId} not found");
    }

    protected RoundDto RoundIdValidation(int roundId)
    {
        var roundDto = Context.Rounds
            .Include(x=>x.Votes)
            .SingleOrDefault(r => r.RoundId == roundId);

        if (roundDto != null) return roundDto;
        Logger.LogWarning("Round with ID {roundId} could not been found", roundId);
        throw new IdNotFoundException($"{typeof(Round)} with ID {roundId} not found");
    }
    

    protected GameRoomPlayer PlayerIdValidationInGameRoom(int playerId, GameRoomDto gameRoomDto)
    {
        var playerDto = gameRoomDto.GameRoomPlayers.SingleOrDefault(gr => gr.PlayerId == playerId);
        if (playerDto != null) return playerDto;
        Logger.LogWarning("Player(ID{PlayerId}) in Game Room (ID{GameRoomId}) was not found", playerId, gameRoomDto.Id);
        throw new IdNotFoundException($"{typeof(Player)} in game room {gameRoomDto.Id} with player ID {playerId} not found");
    }

    protected void ValidateAlreadyExistGameRoom(GameRoom gameRoomRequest)
    {
        if (!Context.GameRooms.Any(x => x.Id == gameRoomRequest.Id)) return;
        Logger.LogWarning("Game room with ID{Id} already exists", gameRoomRequest.Id);
        throw new IdAlreadyExistException($"{typeof(GameRoom)} with {gameRoomRequest.Id} already exist");
    }

    protected void ValidateAlreadyExistPlayer(Player player)
    {
        if (!Context.Players.Any(p => p.Id == player.Id)) return;
        Logger.LogWarning("Player with ID{ID} already exists", player.Id);
        throw new IdAlreadyExistException($"{typeof(Player)} with {player.Id} already exist");
    }
}