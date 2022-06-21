using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.ForbiddenExceptions;
using ScrumPoker.Common.Models;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.Business;

/// <inheritdoc />
public class VoteService : IVoteService
{
    private readonly IGameRoomService _gameRoomService;
    private readonly IRoundService _roundService;
    private readonly IUserManager _userManager;
    private readonly IVoteRepository _voteRepository;

    public VoteService(IVoteRepository voteRepository, IUserManager userManager, IRoundService roundService,
        IGameRoomService gameRoomService)
    {
        _voteRepository = voteRepository;
        _userManager = userManager;
        _roundService = roundService;
        _gameRoomService = gameRoomService;
    }

    public async Task<Vote> GetById(int id)
    {
        return await _voteRepository.GetById(id);
    }

    public async Task<Vote> CreateOrUpdate(Vote vote)
    {
        var roundDto = await _roundService.GetById(vote.RoundId);
        
        if (roundDto.RoundState != RoundState.VoteRegistration)
            throw new InvalidRoundStateException("You are allowed to vote only when round state is - vote registration");
                
        var gameRoomDto = await _gameRoomService.GetById(roundDto.GameRoomId);
        var currentUserId = _userManager.GetCurrentUserId();
        
        var playerList = gameRoomDto.Players;
        var playerCheck = playerList.SingleOrDefault(x => x.Id == currentUserId);
        
        if (playerCheck == null)
            throw new IdNotFoundException($"No user with ID {currentUserId} found in game room ID {gameRoomDto.Id}");

        vote.PlayerId = currentUserId;
        
        return await _voteRepository.CreateOrUpdate(vote);
    }

    public async Task ClearRoundVotes(int roundId)
    {
        var roundDto = await _roundService.GetById(roundId);
        var gameRoomDto = await _gameRoomService.GetById(roundDto.GameRoomId);
        var currentUserId = _userManager.GetCurrentUserId();

        if (gameRoomDto.MasterId != currentUserId)
            throw new ActionNotAllowedException($"User has not rights to Update game room (ID {gameRoomDto.Id})");
        
        if (roundDto.RoundState == RoundState.Finished)
            throw new InvalidRoundStateException("Round is finished, any updates on votes is unavailable!");
        
        await _voteRepository.ClearRoundVotes(roundId);
    }
}