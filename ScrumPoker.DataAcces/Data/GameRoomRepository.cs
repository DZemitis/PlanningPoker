using AutoMapper;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Data.PersistenceMock;
using ScrumPoker.DataAcces.Models.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Data.Data;

/// <inheritdoc />
public class GameRoomRepository : IGameRoomRepository
{
    private static int _id { get; set; }
    private readonly IMapper _mapper;

    public GameRoomRepository(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<GameRoom> GetAll()
    {
        var gameRoomListResponse = _mapper.Map<List<GameRoom>>(TempDb._gameRooms);

        return gameRoomListResponse;
    }

    public GameRoom GetById(int id)
    {
        var gameRoomDto = TempDb._gameRooms.Single(x => x.Id == id);
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public GameRoom Create(GameRoom gameRoomRequest)
    {
        var addGameRoom = new GameRoomDto
        {
            Name = gameRoomRequest.Name,
            Id = ++_id
        };

        var gameRoomDto = _mapper.Map<GameRoomDto>(addGameRoom);
        TempDb._gameRooms.Add(gameRoomDto);

        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = TempDb._gameRooms.Single(x => x.Id == gameRoomRequest.Id);
        gameRoomDto.Name = gameRoomRequest.Name;

        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public void DeleteAll()
    {
        TempDb._gameRooms.Clear();
    }

    public void DeleteById(int id)
    {
        TempDb._gameRooms.RemoveAll(x => x.Id == id);
    }

    public void UpdatePlayerList(GameRoom gameRoomRequest)
    {
        var gameRoomToUpdate = _mapper.Map<GameRoomDto>(gameRoomRequest);
        var gameRoomDto = TempDb._gameRooms.Single(x => x.Id == gameRoomRequest.Id);

        gameRoomDto.Players = gameRoomToUpdate.Players;
    }
    
    public void RemoveGameRoomPlayerById(int gameRoomId, int playerId)
    {
        var gameRoomDto = TempDb._gameRooms.Single(x => x.Id == gameRoomId);
        var playerToRemove = gameRoomDto.Players.Single(x => x.Id == playerId);
        gameRoomDto.Players.Remove(playerToRemove);

        var playerDto = TempDb._playerList.Single(x => x.Id == playerId);
        var gameRoomToRemove = playerDto.GameRooms.Single(x => x.Id == gameRoomId);
        playerDto.GameRooms.Remove(gameRoomToRemove);
    }

    public void AddPlayerToRoom(int gameRoomId, int playerId)
    {
        var gameRoomDto = TempDb._gameRooms.Single(x => x.Id == gameRoomId);
        var playerList = gameRoomDto.Players;

        var playerDto = TempDb._playerList.Single(x => x.Id == playerId);
        var gameRoomList = playerDto.GameRooms;
        
        playerList.Add(playerDto);
        gameRoomList.Add(gameRoomDto);
    }
}