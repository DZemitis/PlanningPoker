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
    protected readonly IMapper _mapper;
    protected readonly IScrumPokerContext _context;
    private readonly ILogger<RepositoryBase> _logger;

    protected RepositoryBase(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }
    protected GameRoomDto GameRoomIdValidation(int gameRoomId)
    {
        var gameRoomDto = _context.GameRooms
            .Include(gr=>gr.GameRoomPlayers)
            .ThenInclude(x=>x.Player)
            .Include(x=>x.Master)
            .Include(x=>x.CurrentRound)
            .SingleOrDefault(g => g.Id == gameRoomId);

        if (gameRoomDto != null) return gameRoomDto;
        _logger.LogWarning("Game Room with ID {Id} could not be found", gameRoomId);
        throw new IdNotFoundException($"{typeof(GameRoom)} with ID {gameRoomId} not found");
    }

    protected PlayerDto PlayerIdValidation(int playerId)
    {
        var playerDto = _context.Players
            .Include(p=>p.PlayerGameRooms)
            .SingleOrDefault(p => p.Id == playerId);

        if (playerDto != null) return playerDto;
        _logger.LogWarning("Player with ID {Id} could not been found", playerId);
        throw new IdNotFoundException($"{typeof(Player)} with ID {playerId} not found");
    }

    protected RoundDto RoundIdValidation(int roundId)
    {
        var roundDto = _context.Rounds.SingleOrDefault(r => r.RoundId == roundId);

        if (roundDto != null) return roundDto;
        _logger.LogWarning("Round with ID {roundId} could not been found", roundId);
        throw new IdNotFoundException($"{typeof(Round)} with ID {roundId} not found");
    }
    

    protected GameRoomPlayer PlayerIdValidationInGameRoom(int playerId, GameRoomDto gameRoomDto)
    {
        var playerDto = gameRoomDto.GameRoomPlayers.SingleOrDefault(gr => gr.PlayerId == playerId);
        if (playerDto != null) return playerDto;
        _logger.LogWarning("Player(ID{PlayerId}) in Game Room (ID{GameRoomId}) was not found", playerId, gameRoomDto.Id);
        throw new IdNotFoundException($"{typeof(Player)} in game room {gameRoomDto.Id} with player ID {playerId} not found");
    }

    protected void ValidateAlreadyExistGameRoom(GameRoom gameRoomRequest)
    {
        if (!_context.GameRooms.Any(x => x.Id == gameRoomRequest.Id)) return;
        _logger.LogWarning("Game room with ID{Id} already exists", gameRoomRequest.Id);
        throw new IdAlreadyExistException($"{typeof(GameRoom)} with {gameRoomRequest.Id} already exist");
    }

    protected void ValidateAlreadyExistPlayer(Player player)
    {
        if (!_context.Players.Any(p => p.Id == player.Id)) return;
        _logger.LogWarning("Player with ID{ID} already exists", player.Id);
        throw new IdAlreadyExistException($"{typeof(Player)} with {player.Id} already exist");
    }

    protected void VoteAlreadyMadeValidation(VoteRegistration voteRequest, DbSet<VoteRegistrationDto> voteRegistrationDto)
    {
        var checkVote =
            voteRegistrationDto.SingleOrDefault(x =>
                x.PlayerId == voteRequest.PlayerId && x.RoundId == voteRequest.RoundId);

        if (checkVote == null) return;
        _logger.LogWarning("Player with ID {Id}, has already voted", voteRequest.PlayerId);
        throw new VoteAlreadyExistException($"Player with ID {voteRequest.PlayerId} has already made his vote");
    }

    protected VoteRegistrationDto VoteNotFoundValidation(VoteRegistrationDto voteRequest)
    {
        var checkVote = _context.Votes.SingleOrDefault(x => x.Id == voteRequest.Id);
        if (checkVote != null) return checkVote;
        throw new IdNotFoundException($"Vote with ID {voteRequest.Id} has not been found");
    }
}