using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

/// <inheritdoc />
public class GameRoomService : IGameRoomService
{
    private readonly IGameRoomRepository _gameRoomRepository;

    public GameRoomService(IGameRoomRepository gameRoomRepository)
    {
        _gameRoomRepository = gameRoomRepository;
    }

    public async Task<List<GameRoom>> GetAll()
    {
        return await _gameRoomRepository.GetAll();
    }

    public async Task<GameRoom> GetById(int id)
    {
        return await _gameRoomRepository.GetById(id);
    }

    public async Task<GameRoom> Create(GameRoom gameRoomRequest)
    {
        var gameRoom = await _gameRoomRepository.Create(gameRoomRequest);
        await _gameRoomRepository.AddPlayerToRoom(gameRoom.Id, gameRoom.MasterId);
        var gameRoomResponse = await _gameRoomRepository.GetById(gameRoom.Id);

        return gameRoomResponse;
    }

    public async Task<GameRoom> Update(GameRoom gameRoomRequest)
    {
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
        await _gameRoomRepository.AddPlayerToRoom(gameRoomId, playerId);
    }

    public async Task RemovePlayer(int gameRoomId, int playerId)
    {
        await _gameRoomRepository.RemovePlayerById(gameRoomId, playerId);
    }
}