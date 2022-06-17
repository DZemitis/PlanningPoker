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
        var gameRoom = _gameRoomRepository.Create(gameRoomRequest);
        _gameRoomRepository.AddPlayerToRoom(gameRoom.Id, gameRoom.MasterId);
        var gameRoomResponse = _gameRoomRepository.GetById(gameRoom.Id);

        return gameRoomResponse;
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
        _gameRoomRepository.RemovePlayerById(gameRoomId, playerId);
    }
}