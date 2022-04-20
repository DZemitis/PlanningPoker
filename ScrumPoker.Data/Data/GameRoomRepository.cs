using ScrumPoker.Core.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Data.Data;

public class GameRoomRepository : IGameRoomRepository
{
    private static List<GameRoom> _gameRooms = new List<GameRoom>();
    
    /// <inheritdoc />
    public GameRoom Create(string name)
    {
        var gameRoom = new GameRoom
        {
            Name = name,
            Id = Guid.NewGuid().ToString("N").Substring(0,6)
        };
        
        _gameRooms.Add(gameRoom);

        return gameRoom;
    }
    
    /// <inheritdoc />
    public List<GameRoom> GetAll()
    {
        return _gameRooms;
    }

    /// <inheritdoc />
    public void Update(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void DeleteAll()
    {
        _gameRooms.Clear();
    }

    /// <inheritdoc />
    public void DeleteById(string id)
    {
        for (int i = 0; i < _gameRooms.Count; i++)
        {
            if (_gameRooms[i].Id.Equals(id))
                _gameRooms.RemoveAt(i);
        }
    }

    /// <inheritdoc />
    public IEnumerable<GameRoom> GetById(string id)
    {
        var gameRoom = _gameRooms.Where(x => x.Id == id);
        
        return gameRoom;
    }
}