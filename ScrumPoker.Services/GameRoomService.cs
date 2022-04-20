using ScrumPoker.Core.Models;
using ScrumPoker.Core.Services;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Services;

/// <inheritdoc />
public class GameRoomService : IGameRoomService
{
    private readonly IGameRoomRepository _gameRoomRepository;

    public GameRoomService(IGameRoomRepository gameRoomRepository)
    {
        _gameRoomRepository = gameRoomRepository;
    }

    public GameRoom Create(GameRoom gameRoomRequest)
    {
        var gameRoom =_gameRoomRepository.Create(gameRoomRequest);

        return gameRoom;
    }
    
    public GameRoom GetById(int id)
    {
        return _gameRoomRepository.GetById(id);
    }

    public List<GameRoom> GetAll()
    {
        return _gameRoomRepository.GetAll();
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
}