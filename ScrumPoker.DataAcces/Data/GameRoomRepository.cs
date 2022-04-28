using AutoMapper;
using ScrumPoker.Business.Models.Models;
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
        var gameRoomListResponse = _mapper.Map<List<GameRoom>>(GameRepository._gameRooms);
        
        return gameRoomListResponse;
    }

    public GameRoom GetById(int id)
    {
        var gameRoomDto = GameRepository._gameRooms.FirstOrDefault(x => x.Id == id);
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
        GameRepository._gameRooms.Add(gameRoomDto);
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);
        
        return gameRoomDtoResponse;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = GameRepository._gameRooms.FirstOrDefault(x => x.Id == gameRoomRequest.Id);
        gameRoomDto.Name = gameRoomRequest.Name;

        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }
    
    public void UpdatePlayerList(GameRoom gameRoomRequest)
    {
        var gameRoomToUpdate = _mapper.Map<GameRoomDto>(gameRoomRequest);
        var gameRoomDto = GameRepository._gameRooms.FirstOrDefault(x => x.Id == gameRoomRequest.Id);
        
        gameRoomDto.Players = gameRoomToUpdate.Players;
    }

    public void DeleteAll()
    {
        GameRepository._gameRooms.Clear();
    }

    public void DeleteById(int id)
    {
        GameRepository._gameRooms.RemoveAll(x => x.Id == id);
    }

    public void RemoveGameRoomPlayerById(int gameRoomId, int playerId)
    {
        var gameRoomDto = GameRepository._gameRooms.FirstOrDefault(x => x.Id == gameRoomId);
        gameRoomDto.Players.RemoveAll(x => x.Id == playerId);

        var playerDto = GameRepository._playerList.FirstOrDefault(x => x.Id == playerId);
        playerDto.GameRooms.RemoveAll(x => x.Id == gameRoomId);

    }
}