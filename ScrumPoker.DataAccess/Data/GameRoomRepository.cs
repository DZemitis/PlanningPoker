using AutoMapper;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.Models;
using ScrumPoker.DataAccess.PersistenceMock;

namespace ScrumPoker.DataAccess.Data;

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
        var gameRoomListResponse = _mapper.Map<List<GameRoom>>(TempDb.GameRooms);

        return gameRoomListResponse;
    }

    public GameRoom GetById(int id)
    {
        var gameRoomDto = GameRoomIdValidation(id);
        
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);
        return gameRoomDtoResponse;
    }

    public GameRoom Create(GameRoom gameRoomRequest)
    {
        ValidateAlreadyExistException(gameRoomRequest);
        
        var addGameRoom = new GameRoomDto
        {
            Name = gameRoomRequest.Name,
            Id = ++_id
        };

        var gameRoomDto = _mapper.Map<GameRoomDto>(addGameRoom);
        TempDb.GameRooms.Add(gameRoomDto);

        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = GameRoomIdValidation(gameRoomRequest.Id);
        
        gameRoomDto.Name = gameRoomRequest.Name;

        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public void DeleteAll()
    {
        TempDb.GameRooms.Clear();
    }

    public void DeleteById(int id)
    {
        GameRoomIdValidation(id);
        
        TempDb.GameRooms.RemoveAll(x => x.Id == id);
    }

    public void RemoveGameRoomPlayerById(int gameRoomId, int playerId)
    {
        var gameRoomDto = GameRoomIdValidation(gameRoomId);

        var playerToRemove = PlayerIdValidationInGameRoom(playerId, gameRoomDto);

        gameRoomDto.Players.Remove(playerToRemove);

        var playerDto = TempDb.PlayerList.Single(x => x.Id == playerId);
        var gameRoomToRemove = playerDto.GameRooms.Single(x => x.Id == gameRoomId);
        
        playerDto.GameRooms.Remove(gameRoomToRemove);
    }

    public void AddPlayerToRoom(int gameRoomId, int playerId)
    {
        var gameRoomDto = GameRoomIdValidation(gameRoomId);
        
        var playerList = gameRoomDto.Players;

        var playerDto = PlayerIdValidation(playerId);

        var gameRoomList = playerDto.GameRooms;

        playerList.Add(playerDto);
        gameRoomList.Add(gameRoomDto);
    }

    private static void ValidateAlreadyExistException(GameRoom gameRoomRequest)
    {
        if (TempDb.GameRooms.Any(x => x.Id == gameRoomRequest.Id))
        {
            throw new IdAlreadyExistException($"{typeof(GameRoom)} with {gameRoomRequest.Id} already exist");
        }
    }

    private static GameRoomDto GameRoomIdValidation(int gameRoomId)
    {
        var gameRoomDto = TempDb.GameRooms.SingleOrDefault(x => x.Id == gameRoomId);
        if (gameRoomDto == null)
        {
            throw new IdNotFoundException($"{typeof(GameRoom)} with ID {gameRoomId} not found");
        }

        return gameRoomDto;
    }

    private static PlayerDto PlayerIdValidation(int playerId)
    {
        var playerDto = TempDb.PlayerList.SingleOrDefault(p => p.Id == playerId);
        if (playerDto == null)
        {
            throw new IdNotFoundException($"{typeof(Player)} with ID {playerId} not found");
        }

        return playerDto;
    }

    private static PlayerDto PlayerIdValidationInGameRoom(int playerId, GameRoomDto gameRoomDto)
    {
        var playerDto = gameRoomDto.Players.SingleOrDefault(x => x.Id == playerId);
        if (playerDto == null)
        {
            throw new IdNotFoundException($"{typeof(Player)} in game room {gameRoomDto.Id} with player ID {playerId} not found");
        }

        return playerDto;
    }
}