using ScrumPoker.Core.Models;
using ScrumPoker.Core.Services;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Services;

public class GameRoomService : IGameRoomService
{
    private readonly IGameRoomRepository _gameRoomRepository;

    public GameRoomService(IGameRoomRepository gameRoomRepository)
    {
        _gameRoomRepository = gameRoomRepository;
    }

    /// <inheritdoc />
    public GameRoom Create(GameRoom gameRoomRequest)
    {
        var gameRoom =_gameRoomRepository.Create(gameRoomRequest);

        return gameRoom;
    }
    
    /// <inheritdoc />
    public IEnumerable<GameRoom> GetById(int id)
    {
        return _gameRoomRepository.GetById(id);
    }

    /// <inheritdoc />
    public List<GameRoom> GetAll()
    {
        return _gameRoomRepository.GetAll();
    }

    /// <inheritdoc />
    public void Update(int id)
    {
        _gameRoomRepository.Update(id);
    }

    /// <inheritdoc />
    public void DeleteAll()
    {
        _gameRoomRepository.DeleteAll();
    }

    /// <inheritdoc />
    public void DeleteById(int id)
    {
        _gameRoomRepository.DeleteById(id);
    }
}