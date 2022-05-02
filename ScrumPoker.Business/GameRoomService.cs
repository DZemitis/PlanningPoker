using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Services;

/// <inheritdoc />
public class GameRoomService : IGameRoomService
{
    private readonly IGameRoomRepository _gameRoomRepository;
    private readonly IPlayerRepository _playerRepository;

    public GameRoomService(IGameRoomRepository gameRoomRepository, IPlayerRepository playerRepository)
    {
        _gameRoomRepository = gameRoomRepository;
        _playerRepository = playerRepository;
    }

    public List<GameRoom> GetAll()
    {
        return _gameRoomRepository.GetAll();
    }

    public GameRoom GetById(int id)
    {
        return _gameRoomRepository.GetById(id);
    }

    public GameRoom Create(GameRoom gameRoomRequest)
    {
        var gameRoom =_gameRoomRepository.Create(gameRoomRequest);

        return gameRoom;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        return _gameRoomRepository.Update(gameRoomRequest);
    }

    public void DeleteAll()
    {
        _gameRoomRepository.DeleteAll();
    }

    public void DeleteById(int id)
    {
        _gameRoomRepository.DeleteById(id);
    }

    public void AddPlayer(int gameRoomId, int playerId)
    {
        _gameRoomRepository.AddPlayerToRoom(gameRoomId, playerId);
    }

    public void RemovePlayer(int gameRoomId, int playerId)
    {
        _gameRoomRepository.RemoveGameRoomPlayerById(gameRoomId, playerId);
    }
}