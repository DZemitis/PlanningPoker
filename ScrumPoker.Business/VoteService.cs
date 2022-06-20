using Microsoft.IdentityModel.Tokens;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

/// <inheritdoc />
public class VoteService : IVoteService
{
    private readonly IVoteRepository _voteRepository;
    private readonly IUserManager _userManager;
    private readonly IGameRoomService _gameRoomService;
    private readonly IRoundService _roundService;

    public VoteService(IVoteRepository voteRepository, IUserManager userManager, IRoundService roundService, IGameRoomService gameRoomService)
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
        var gameRoomDto = await _gameRoomService.GetById(roundDto.GameRoomId);
        var playerId = _userManager.GetUserId();
        var playerList = gameRoomDto.Players;
        var playerCheck = playerList.SingleOrDefault(x => x.Id == playerId);
        if (playerCheck == null)
        {
            throw new IdNotFoundException($"No user with ID {playerId} found in game room ID {gameRoomDto.Id}");
        }
        
        vote.PlayerId = playerId;
        return await _voteRepository.CreateOrUpdate(vote);
    }

    public async Task ClearRoundVotes(int roundId)
    {
        await _voteRepository.ClearRoundVotes(roundId);
    }
}