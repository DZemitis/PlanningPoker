using ScrumPoker.Core.Models;
using ScrumPoker.Core.Services;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Services;

public class GameRoomService : IGameRoomService
{
    private readonly IGameRoomData _gameRoomData;

    public GameRoomService(IGameRoomData gameRoomData)
    {
        _gameRoomData = gameRoomData;
    }

    public void CreateGameRoom(string name)
    {
        _gameRoomData.Create(name);
    }
    
    public IEnumerable<GameRoom> GetGameRoomByName(string name)
    {
        return _gameRoomData.GetGameRoomByName(name);
    }

    public List<GameRoom> GetAllGameRooms()
    {
        return _gameRoomData.GetAllGameRooms();
    }

    public void DeleteAllGameRooms()
    {
        _gameRoomData.DeleteAllRooms();
    }

    public void DeleteGameRoomByName(string name)
    {
        _gameRoomData.DeleteRoomByName(name);
    }
}