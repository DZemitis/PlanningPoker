using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
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

    public List<GameRoom> GetAll()
    {
        var gameRooms = Context.GameRooms
            .Include(gr => gr.GameRoomPlayers).ThenInclude(p => p.Player)
            .Include(gr => gr.Master)
            .Include(x => x.CurrentRound);

        var gameRoomListResponse = Mapper.Map<List<GameRoom>>(gameRooms);

        return gameRoomListResponse;
    }

    public GameRoom GetById(int id)
    {
        var gameRoomDto = GetGameRoomById(id);

        var gameRoomDtoResponse = Mapper.Map<GameRoom>(gameRoomDto);
        return gameRoomDtoResponse;
    }

    public GameRoom Create(GameRoom gameRoomRequest)
    {
        var masterPLayer = GetPlayerById(gameRoomRequest.MasterId);

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

        Context.Rounds.Add(initialRound);
        Context.SaveChanges();
        addGameRoom.CurrentRound = initialRound;
        Context.SaveChanges();

        var gameRoomDtoResponse = Mapper.Map<GameRoom>(addGameRoom);

        return gameRoomDtoResponse;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = GetGameRoomById(gameRoomRequest.Id);

        gameRoomDto.Name = gameRoomRequest.Name;
        Context.SaveChanges();

        var gameRoomDtoResponse = Mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public void DeleteAll()
    {
        Context.Rounds.RemoveRange(Context.Rounds);
        Context.SaveChanges();

        Context.GameRooms.RemoveRange(Context.GameRooms);
        Context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var roundList = Context.Rounds.Where(x => x.GameRoomId == id);
        Context.Rounds.RemoveRange(roundList);
        Context.SaveChanges();

        var gameRoomDto = GetGameRoomById(id);
        Context.GameRooms.Remove(gameRoomDto);
        Context.SaveChanges();
    }

    public void RemovePlayerById(int gameRoomId, int playerId)
    {
        var gameRoomDto = GetGameRoomById(gameRoomId);

        var playerToRemove = gameRoomDto.GameRoomPlayers.SingleOrDefault(x => x.PlayerId == playerId);
        if (playerToRemove == null)
        {
            Logger.LogWarning
            ("Player(ID{PlayerId}) in Game Room (ID{GameRoomId}) was not found",
                playerId, gameRoomDto.Id);
            throw new IdNotFoundException
                ($"{typeof(Player)} in game room {gameRoomDto.Id} with player ID {playerId} not found");
        }

        var gameRoomPlayerDto = Context.GameRoomsPlayers.SingleOrDefault(x =>
            x.GameRoomId == gameRoomId && x.PlayerId == playerId);

        Context.GameRoomsPlayers.RemoveRange(gameRoomPlayerDto!);
        Context.SaveChanges();
    }

    public void AddPlayerToRoom(int gameRoomId, int playerId)
    {
        var gameRoomDto = GetGameRoomById(gameRoomId);

        var playerDto = GetPlayerById(playerId);

        var gameRoomPlayers = new GameRoomPlayer
        {
            Player = playerDto,
            GameRoom = gameRoomDto
        };

        Context.GameRoomsPlayers.Add(gameRoomPlayers);
        Context.SaveChanges();
    }
}