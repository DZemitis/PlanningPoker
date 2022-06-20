using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

/// <inheritdoc />
public class GameRoomService : IGameRoomService
{
    private readonly IGameRoomRepository _gameRoomRepository;
    private readonly IUserManager _userManager;

    public GameRoomService(IGameRoomRepository gameRoomRepository, IUserManager userManager)
    {
        _gameRoomRepository = gameRoomRepository;
        _userManager = userManager;
    }

    public async Task<List<GameRoom>> GetAll()
    {
        return await _gameRoomRepository.GetAll();
    }

    public async Task<GameRoom> GetById(int id)
    {
        return await _gameRoomRepository.GetById(id);
    }

    public async Task<GameRoom> Create(GameRoom gameRoomRequest, int requestId)
    {
        var masterId = _userManager.GetUserId();
        gameRoomRequest.MasterId = masterId;
        var gameRooms = await _gameRoomRepository.Create(gameRoomRequest);
        await _gameRoomRepository.AddPlayerToRoom(gameRooms.Id, gameRooms.MasterId);
        var gameRoomResponse = await _gameRoomRepository.GetById(gameRooms.Id);

        return gameRoomResponse;
    }

    public async Task<GameRoom> Update(GameRoom gameRoomRequest)
    {
        var gameRoomDto = await GetById(gameRoomRequest.Id);
        var masterId = _userManager.GetUserId();
        if (gameRoomDto.MasterId != masterId)
        {
            throw new HasNoClaimException($"User has not rights to Update game room (ID {gameRoomRequest.Id})");
        }
        
        return await _gameRoomRepository.Update(gameRoomRequest);
    }

    public async Task DeleteAll()
    {
        await _gameRoomRepository.DeleteAll();
    }

    public async Task DeleteById(int id)
    {
        await _gameRoomRepository.DeleteById(id);
    }

    public async Task AddPlayer(int gameRoomId, int playerId)
    {
        var gameRoomDto = await GetById(gameRoomId);
        var masterId = _userManager.GetUserId();
        if (gameRoomDto.MasterId != masterId)
        {
            throw new HasNoClaimException($"User has not rights to Update game room (ID {gameRoomId})");
        }
        
        await _gameRoomRepository.AddPlayerToRoom(gameRoomId, playerId);
    }

    public async Task RemovePlayer(int gameRoomId, int playerId)
    {
        var gameRoomDto = await GetById(gameRoomId);
        var masterId = _userManager.GetUserId();
        if (gameRoomDto.MasterId != masterId)
        {
            throw new HasNoClaimException($"User has not rights to Update game room (ID {gameRoomId})");
        }

        await _gameRoomRepository.RemovePlayerById(gameRoomId, playerId);
    }
}