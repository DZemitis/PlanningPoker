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
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger _logger;

    protected RepositoryBase(IMapper mapper, IScrumPokerContext context, ILogger logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }
    public GameRoomDto GameRoomIdValidation(int gameRoomId)
    {
        var gameRoomDto = _context.GameRooms
            .Include(gr=>gr.GameRoomPlayers)
            .ThenInclude(x=>x.Player)
            .Include(x=>x.Master)
            .Include(x=>x.CurrentRound)
            .SingleOrDefault(g => g.Id == gameRoomId);
        
        if (gameRoomDto == null)
        {
            _logger.LogWarning("Game Room with ID {Id} could not be found", gameRoomId);
            throw new IdNotFoundException($"{typeof(GameRoom)} with ID {gameRoomId} not found");
        }

        return gameRoomDto;
    }

    public PlayerDto PlayerIdValidation(int playerId)
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

    public GameRoomPlayer PlayerIdValidationInGameRoom(int playerId, GameRoomDto gameRoomDto)
    {
        var playerDto = gameRoomDto.GameRoomPlayers.SingleOrDefault(gr => gr.PlayerId == playerId);
        if (playerDto == null)
        {
            _logger.LogWarning("Player(ID{PlayerId}) in Game Room (ID{GameRoomId}) was not found", playerId, gameRoomDto.Id);
            throw new IdNotFoundException($"{typeof(Player)} in game room {gameRoomDto.Id} with player ID {playerId} not found");
        }

        return playerDto;
    }
    
    public void ValidateAlreadyExistGameRoom(GameRoom gameRoomRequest)
    {
        if (_context.GameRooms.Any(x => x.Id == gameRoomRequest.Id))
        {
            _logger.LogWarning("Game room with ID{Id} already exists", gameRoomRequest.Id);
            throw new IdAlreadyExistException($"{typeof(GameRoom)} with {gameRoomRequest.Id} already exist");
        }
    }
    
    public void ValidateAlreadyExistPlayer(Player player)
    {
        if (_context.Players.Any(p => p.Id == player.Id))
        {
            _logger.LogWarning("Player with ID{ID} already exists", player.Id);
            throw new IdAlreadyExistException($"{typeof(Player)} with {player.Id} already exist");
        }
    }
    
    protected GameRoomDto AddPlayerGameRoomIdValidation(int gameRoomId)
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
}