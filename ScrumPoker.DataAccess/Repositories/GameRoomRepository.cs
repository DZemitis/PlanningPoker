using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.Models;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Repositories;

/// <inheritdoc cref="ScrumPoker.DataAccess.Interfaces.IGameRoomRepository" />
public class GameRoomRepository : RepositoryBase, IGameRoomRepository
{
    public GameRoomRepository(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger) : base(mapper,
        context, logger)
    {
    }

    public async Task<List<GameRoom>> GetAll()
    {
        var gameRooms = await Context.GameRooms
            .Include(gr => gr.GameRoomPlayers).ThenInclude(p => p.Player)
            .Include(gr => gr.Master)
            .Include(x => x.CurrentRound).ToListAsync();

        var gameRoomListResponse = Mapper.Map<List<GameRoom>>(gameRooms);

        return gameRoomListResponse;
    }

    public async Task<GameRoom> GetById(int id)
    {
        var gameRoomDto = await GetGameRoomById(id);

        var gameRoomDtoResponse = Mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public async Task<GameRoom> Create(GameRoom gameRoomRequest)
    {
        var masterPLayer = await GetPlayerById(gameRoomRequest.MasterId);

        var addGameRoom = new GameRoomDto
        {
            Name = gameRoomRequest.Name,
            Master = masterPLayer
        };

        var initialRound = new RoundDto
        {
            RoundState = RoundState.Grooming,
            Description = gameRoomRequest.Round.Description,
            GameRoom = addGameRoom
        };

        await Context.Rounds.AddAsync(initialRound);
        await Context.SaveChangesAsync();
        addGameRoom.CurrentRound = initialRound;
        await Context.SaveChangesAsync();

        var gameRoomDtoResponse = Mapper.Map<GameRoom>(addGameRoom);

        return gameRoomDtoResponse;
    }

    public async Task<GameRoom> Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = await GetGameRoomById(gameRoomRequest.Id);

        gameRoomDto.Name = gameRoomRequest.Name;
        await Context.SaveChangesAsync();

        var gameRoomDtoResponse = Mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public async Task DeleteAll()
    {
        Context.Rounds.RemoveRange(Context.Rounds);
        await Context.SaveChangesAsync();

        Context.GameRooms.RemoveRange(Context.GameRooms);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var roundList = Context.Rounds.Where(x => x.GameRoomId == id);
        Context.Rounds.RemoveRange(roundList);
        await Context.SaveChangesAsync();

        var gameRoomDto = await GetGameRoomById(id);
        Context.GameRooms.Remove(gameRoomDto);
        await Context.SaveChangesAsync();
    }

    public async Task RemovePlayerById(int gameRoomId, int playerId)
    {
        var gameRoomDto = await GetGameRoomById(gameRoomId);

        var playerToRemove = gameRoomDto.GameRoomPlayers.SingleOrDefault(x => x.PlayerId == playerId);
        if (playerToRemove == null)
        {
            Logger.LogWarning
            ("Player(ID{PlayerId}) in Game Room (ID{GameRoomId}) was not found",
                playerId, gameRoomDto.Id);
            throw new IdNotFoundException
                ($"{typeof(Player)} in game room {gameRoomDto.Id} with player ID {playerId} not found");
        }

        var gameRoomPlayerDto = await Context.GameRoomsPlayers.SingleOrDefaultAsync(x =>
            x.GameRoomId == gameRoomId && x.PlayerId == playerId);

        Context.GameRoomsPlayers.RemoveRange(gameRoomPlayerDto!);
        await Context.SaveChangesAsync();
    }

    public async Task AddPlayerToRoom(int gameRoomId, int playerId)
    {
        var gameRoomDto = await GetGameRoomById(gameRoomId);

        var playerDto = await GetPlayerById(playerId);
        var gameRoomPlayer = await
            Context.GameRoomsPlayers.SingleOrDefaultAsync(x =>
                x.GameRoomId == gameRoomId && x.PlayerId == playerId);

        if (gameRoomPlayer != null)
        {
            Logger.LogWarning("Player(ID{PlayerId}) in Game Room (ID{GameRoomId}) already exist",
                playerId, gameRoomId);
            throw new IdAlreadyExistException(
                $"{typeof(Player)} in game room {gameRoomId} with player ID {playerId} already exist");
        }

        var gameRoomPlayers = new GameRoomPlayer
        {
            Player = playerDto,
            GameRoom = gameRoomDto
        };

        await Context.GameRoomsPlayers.AddAsync(gameRoomPlayers);
        await Context.SaveChangesAsync();
    }
}