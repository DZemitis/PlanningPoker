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
public class GameRoomRepository : IGameRoomRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger<GameRoomRepository> _logger;
    private readonly IValidation _validator;

    public GameRoomRepository(IMapper mapper, IScrumPokerContext context, ILogger<GameRoomRepository> logger, IValidation validator)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
        _validator = validator;
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
        var gameRoomDto = _validator.GameRoomIdValidation(id);
        
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);
        return gameRoomDtoResponse;
    }

    public GameRoom Create(GameRoom gameRoomRequest)
    {
        _validator.ValidateAlreadyExistGameRoom(gameRoomRequest);
        _validator.PlayerIdValidation(gameRoomRequest.MasterId);
        var Round = _context.Rounds;
        
        var initialRound = new RoundDto
        {
            RoundState = RoundState.Grooming,
            Description = gameRoomRequest.Round.Description
        };
        
        _context.Rounds.Add(initialRound);
        _context.SaveChanges();
        
        var addGameRoom = new GameRoomDto
        {
            Name = gameRoomRequest.Name,
            Master = _context.Players.Single(x=>x.Id == gameRoomRequest.MasterId),
            CurrentRound = initialRound,
            CurrentRoundId = initialRound.RoundId
        };

        var gameRoomDto = _mapper.Map<GameRoomDto>(addGameRoom);
        _context.GameRooms.Add(gameRoomDto);
        _context.SaveChanges();
        
        var gameRoomDtoResponse = _mapper.Map<GameRoom>(gameRoomDto);
        
        initialRound.GameRoomId = gameRoomDtoResponse.Id;
        _context.SaveChanges();
        
        return gameRoomDtoResponse;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(gameRoomRequest.Id);

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
        var gameRoomDto = _validator.GameRoomIdValidation(id);
        _context.GameRooms.Remove(gameRoomDto);
        _context.SaveChanges();
    }

    public void RemoveGameRoomPlayerById(int gameRoomId, int playerId)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(gameRoomId);

        var playerToRemove = _validator.PlayerIdValidationInGameRoom(playerId, gameRoomDto);

        
        gameRoomDto.GameRoomPlayers.Remove(playerToRemove);

        var playerDto = _validator.PlayerIdValidation(playerId);
        var gameRoomToRemove = playerDto.PlayerGameRooms.Single(x => x.GameRoomId == gameRoomId);
        
        playerDto.PlayerGameRooms.Remove(gameRoomToRemove);
        _context.SaveChanges();
    }

    public void AddPlayerToRoom(int gameRoomId, int playerId)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(gameRoomId);
        
        var playerList = gameRoomDto.GameRoomPlayers;

        var playerDto = _validator.PlayerIdValidation(playerId);

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
}