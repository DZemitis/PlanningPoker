using AutoMapper;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAcces.Models.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Data.Data;

/// <inheritdoc />
public class GameRoomRepository : IGameRoomRepository
{
    private static readonly List<GameRoomDto> _gameRooms = new List<GameRoomDto>();
    private static int _id { get; set; }
    private readonly IMapper _mapper;
    
    public GameRoomRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public GameRoom Create(GameRoom gameRoomRequest)
    {
        var addGameRoom = new GameRoomDto
        {
            Name = gameRoomRequest.Name,
            Id = ++_id
        };
        
        var gameRoomDto = _mapper.Map<GameRoomDto>(addGameRoom);
        _gameRooms.Add(gameRoomDto);
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);
        
        return gameRoomDtoResponse;
    }
    
    public List<GameRoom> GetAll()
    {
        var gameRoomList = new List<GameRoom>();
        foreach (var gameRoomDto in _gameRooms)
        {
            var gameRoom = _mapper.Map<GameRoom>(gameRoomDto);
            gameRoomList.Add(gameRoom);
        }
        
        return gameRoomList;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = _gameRooms.FirstOrDefault(x => x.Id == gameRoomRequest.Id);
        gameRoomDto.Name = gameRoomRequest.Name;

        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public void DeleteAll()
    {
        _gameRooms.Clear();
    }

    public void DeleteById(int id)
    {
        for (int i = 0; i < _gameRooms.Count; i++)
        {
            if (_gameRooms[i].Id.Equals(id))
                _gameRooms.RemoveAt(i);
        }
    }

    public GameRoom GetById(int id)
    {
        var gameRoomDto = _gameRooms.FirstOrDefault(x => x.Id == id);
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);
        
        return gameRoomDtoResponse;
    }

    public void AddPlayer(GameRoom gameRoomRequest)
    {
        var gameRoomToUpdate = _mapper.Map<GameRoomDto>(gameRoomRequest);
        var gameRoomDto = _gameRooms.FirstOrDefault(x => x.Id == gameRoomRequest.Id);
        gameRoomDto.Players = gameRoomToUpdate.Players;
    }
}