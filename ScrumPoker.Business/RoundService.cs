using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

/// <inheritdoc />
public class RoundService : IRoundService
{
    private readonly IRoundRepository _roundRepository;
    private readonly IGameRoomService _gameRoomService;
    private readonly IUserManager _userManager;

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
        var roundDto = await GetById(roundRequest.RoundId);
        var gameRoomDto = await _gameRoomService.GetById(roundDto.GameRoomId);
        var masterId = _userManager.GetUserId();
        if (gameRoomDto.MasterId != masterId)
        {
            throw new HasNoClaimException($"User has not rights to Update game room (ID {gameRoomDto.Id})");
        }
        
        var round = await _roundRepository.Create(roundRequest);

        return round;
    }

    public async Task SetState(Round roundRequest)
    {
        var roundDto = await GetById(roundRequest.RoundId);
        var gameRoomDto = await _gameRoomService.GetById(roundDto.GameRoomId);
        var masterId = _userManager.GetUserId();
        if (gameRoomDto.MasterId != masterId)
        {
            throw new HasNoClaimException($"User has not rights to Update game room (ID {gameRoomDto.Id})");
        }
        
        await _roundRepository.SetState(roundRequest);
    }

    public async Task Update(Round roundRequest)
    {
        var roundDto = await GetById(roundRequest.RoundId);
        var gameRoomDto = await _gameRoomService.GetById(roundDto.GameRoomId);
        var masterId = _userManager.GetUserId();
        if (gameRoomDto.MasterId != masterId)
        {
            throw new HasNoClaimException($"User has not rights to Update game room (ID {gameRoomDto.Id})");
        }
        
        await _roundRepository.Update(roundRequest);
    }
}