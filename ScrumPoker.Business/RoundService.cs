using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.ForbiddenExceptions;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

/// <inheritdoc />
public class RoundService : IRoundService
{
    private readonly IGameRoomService _gameRoomService;
    private readonly IRoundRepository _roundRepository;
    private readonly IUserManager _userManager;
    
    private static readonly IReadOnlyList<RoundState> Grooming = new List<RoundState>
    {
        RoundState.VoteRegistration
    };
    
    private static readonly IReadOnlyList<RoundState> VoteRegistration = new List<RoundState>
    {
        RoundState.VoteReview
    };
    
    private static readonly IReadOnlyList<RoundState> VoteReview = new List<RoundState>
    {
        RoundState.VoteRegistration,
        RoundState.Finished
    };

    private static readonly IReadOnlyList<RoundState> Finished = new List<RoundState>();

    private static IEnumerable<RoundState> CheckRoundStates(Round roundRequest)
    {
        var result = roundRequest.RoundState switch
        {
            RoundState.Grooming => Grooming,
            RoundState.VoteRegistration => VoteRegistration,
            RoundState.VoteReview => VoteReview,
            RoundState.Finished => Finished,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        return result;
    }

    public RoundService(IRoundRepository roundRepository, IGameRoomService gameRoomService, IUserManager userManager)
    {
        _roundRepository = roundRepository;
        _gameRoomService = gameRoomService;
        _userManager = userManager;
    }

    public async Task<Round> GetById(int id)
    {
        var round = await _roundRepository.GetById(id);
        
        return round;
    }

    public async Task<Round> Create(Round roundRequest)
    {
        var gameRoomDto = await _gameRoomService.GetById(roundRequest.GameRoomId);
        var currentUserId = _userManager.GetCurrentUserId();
        
        if (gameRoomDto.MasterId != currentUserId)
            throw new ActionNotAllowedException($"User has not rights to Update game room (ID {gameRoomDto.Id})");

        var round = await _roundRepository.Create(roundRequest);

        return round;
    }

    public async Task SetState(Round roundRequest)
    {
        var roundDto = await GetById(roundRequest.RoundId);
        var gameRoomDto = await _gameRoomService.GetById(roundDto.GameRoomId);
        var currentUserId = _userManager.GetCurrentUserId();

        if (roundDto.RoundState == RoundState.Finished)
            throw new InvalidRoundStateException("Round is finished, state cannot be changed!");
        
        if (gameRoomDto.MasterId != currentUserId)
            throw new ActionNotAllowedException($"User has not rights to Update game room (ID {gameRoomDto.Id})");

        if (!CheckRoundStates(roundDto).Contains(roundRequest.RoundState))
            throw new InvalidRoundStateException($"Round state {roundRequest.RoundState.ToString()} is not allowed after {roundDto.RoundState.ToString()}");

        await _roundRepository.SetState(roundRequest);
    }

    public async Task Update(Round roundRequest)
    {
        var roundDto = await GetById(roundRequest.RoundId);
        var gameRoomDto = await _gameRoomService.GetById(roundDto.GameRoomId);
        var currentUserId = _userManager.GetCurrentUserId();
        
        if (gameRoomDto.MasterId != currentUserId)
            throw new ActionNotAllowedException($"User has not rights to Update game room (ID {gameRoomDto.Id})");

        await _roundRepository.Update(roundRequest);
    }
}