using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

/// <inheritdoc />
public class GameRoomRepository : IGameRoomRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger<GameRoomRepository> _logger;

    public GameRoomRepository(IMapper mapper, IScrumPokerContext context, ILogger<GameRoomRepository> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public List<GameRoom> GetAll()
    {
        var gameRooms = _context.GameRooms
            .Include(gr => gr.GameRoomPlayers).ThenInclude(p=>p.Player);
        
        var gameRoomListResponse = _mapper.Map<List<GameRoom>>(gameRooms);
        
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
            Name = gameRoomRequest.Name
        };

        var gameRoomDto = _mapper.Map<GameRoomDto>(addGameRoom);
        _context.GameRooms.Add(gameRoomDto);
        _context.SaveChanges();
        
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = GameRoomIdValidation(gameRoomRequest.Id);

        gameRoomDto.Name = gameRoomRequest.Name;
        _context.SaveChanges();

        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);

        return gameRoomDtoResponse;
    }

    public void DeleteAll()
    {
        _context.GameRooms.RemoveRange(_context.GameRooms);
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var gameRoomDto = GameRoomIdValidation(id);
        _context.GameRooms.Remove(gameRoomDto);
        _context.SaveChanges();
    }

    public void RemoveGameRoomPlayerById(int gameRoomId, int playerId)
    {
        var gameRoomDto = GameRoomIdValidation(gameRoomId);

        var playerToRemove = PlayerIdValidationInGameRoom(playerId, gameRoomDto);

        
        gameRoomDto.GameRoomPlayers.Remove(playerToRemove);

        var playerDto = PlayerIdValidation(playerId);
        var gameRoomToRemove = playerDto.PlayerGameRooms.Single(x => x.GameRoomId == gameRoomId);
        
        playerDto.PlayerGameRooms.Remove(gameRoomToRemove);
        _context.SaveChanges();
    }

    public void AddPlayerToRoom(int gameRoomId, int playerId)
    {
        var gameRoomDto = GameRoomIdValidation(gameRoomId);
        
        var playerList = gameRoomDto.GameRoomPlayers;

        var playerDto = PlayerIdValidation(playerId);

        var gameRoomList = playerDto.PlayerGameRooms;
        
        var gameRoomPlayers = new GameRoomPlayer
        {
            Player = playerDto,
            PlayerId = playerDto.Id,
            GameRoom = gameRoomDto,
            GameRoomId = gameRoomDto.Id
        };
        
        playerList.Add(gameRoomPlayers);
        gameRoomList.Add(gameRoomPlayers);
        _context.SaveChanges();
    }

    private void ValidateAlreadyExistException(GameRoom gameRoomRequest)
    {
        if (_context.GameRooms.Any(x => x.Id == gameRoomRequest.Id))
        {
            _logger.LogWarning("Game room with ID{Id} already exists", gameRoomRequest.Id);
            throw new IdAlreadyExistException($"{typeof(GameRoom)} with {gameRoomRequest.Id} already exist");
        }
    }

    private GameRoomDto GameRoomIdValidation(int gameRoomId)
    {
        var gameRoomDto = _context.GameRooms
            .Include(gr=>gr.GameRoomPlayers).ThenInclude(x=>x.Player)
            .SingleOrDefault(g => g.Id == gameRoomId);
        
        if (gameRoomDto == null)
        {
            _logger.LogWarning("Game Room with ID {Id} could not be found", gameRoomId);
            throw new IdNotFoundException($"{typeof(GameRoom)} with ID {gameRoomId} not found");
        }

        return gameRoomDto;
    }

    private PlayerDto PlayerIdValidation(int playerId)
    {
        var playerDto = _context.Players
            .Include(p=>p.PlayerGameRooms)
            .SingleOrDefault(p => p.Id == playerId);
        
        if (playerDto == null)
        {
            _logger.LogWarning("Player with ID {Id} could not been found", playerId);
            throw new IdNotFoundException($"{typeof(Player)} with ID {playerId} not found");
        }

        return playerDto;
    }

    private GameRoomPlayer PlayerIdValidationInGameRoom(int playerId, GameRoomDto gameRoomDto)
    {
        var playerDto = gameRoomDto.GameRoomPlayers.SingleOrDefault(gr => gr.PlayerId == playerId);
        if (playerDto == null)
        {
            _logger.LogWarning("Player(ID{PlayerId}) in Game Room (ID{GameRoomId}) was not found", playerId, gameRoomDto.Id);
            throw new IdNotFoundException($"{typeof(Player)} in game room {gameRoomDto.Id} with player ID {playerId} not found");
        }

        return playerDto;
    }
}