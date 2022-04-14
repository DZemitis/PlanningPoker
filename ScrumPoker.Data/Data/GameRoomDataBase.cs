using ScrumPoker.Core.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Data.Data;

public class GameRoomDataBase : IGameRoomData
{
    private static List<GameRoom> _gameRooms = new List<GameRoom>();
    private static int Id { get; set; }
    
    public void Create(string name)
    {
        var gameRoom = new GameRoom
        {
            Name = name,
            Id = ++Id
        };
        
        _gameRooms.Add(gameRoom);
    }

    public List<GameRoom> GetAllGameRooms()
    {
        return _gameRooms;
    }

    public void DeleteAllRooms()
    {
        _gameRooms.Clear();
    }

    public void DeleteRoomByName(string name)
    {
        for (int i = 0; i < GameRoomDataBase._gameRooms.Count; i++)
        {
            if (_gameRooms[i].Name.Equals(name))
                _gameRooms.RemoveAt(i);
        }
    }

    public IEnumerable<GameRoom> GetGameRoomByName(string name)
    {
        var gameRoom = _gameRooms.Where(x => x.Name == name);
        
        return gameRoom;
    }
}