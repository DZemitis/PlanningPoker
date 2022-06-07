using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

/// <inheritdoc />
public class GameRoomRepository : RepositoryBase ,IGameRoomRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger<GameRoomRepository> _logger;

    public GameRoomRepository(IMapper mapper, IScrumPokerContext context, ILogger<GameRoomRepository> logger) : base(mapper, context, logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public List<GameRoom> GetAll()
    {
        var gameRooms = _context.GameRooms
            .Include(gr => gr.GameRoomPlayers).ThenInclude(p => p.Player)
            .Include(gr => gr.Master)
            .Include(x=>x.CurrentRound);
        
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
        ValidateAlreadyExistGameRoom(gameRoomRequest);
        PlayerIdValidation(gameRoomRequest.MasterId);
        
        var initialRound = new RoundDto
        {
            RoundState = RoundState.Grooming,
            Description = gameRoomRequest.Round.Description
        };
        
        var addGameRoom = new GameRoomDto
        {
            Name = gameRoomRequest.Name,
            Master = _context.Players.Single(x=>x.Id == gameRoomRequest.MasterId),
            CurrentRound = initialRound,
        };
        
        _context.GameRooms.Add(addGameRoom);
        _context.SaveChanges();
        
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(addGameRoom);
        
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
        _context.Players.RemoveRange(_context.Players);
        _context.Rounds.RemoveRange(_context.Rounds);
        _context.Votes.RemoveRange(_context.Votes);
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
        
        var gameRoomPlayers = new GameRoomPlayer
        {
            Player = playerDto,
            GameRoom = gameRoomDto,
        };

        /*_context.GameRoomsPlayers.Add(gameRoomPlayers);*/
        playerList.Add(gameRoomPlayers);
        _context.SaveChanges();
    }
}