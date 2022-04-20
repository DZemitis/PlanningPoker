using ScrumPoker.Core.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Data.Data;

public class GameRoomRepository : IGameRoomRepository
{
    private static readonly List<GameRoom> _gameRooms = new List<GameRoom>();
    private static int Id { get; set; }
    
    /// <inheritdoc />
    public GameRoom Create(GameRoom gameRoomRequest)
    {
        var gameRoom = new GameRoom
        {
            Name = gameRoomRequest.Name,
            Id = ++Id
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
    public void Update(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void DeleteAll()
    {
        _gameRooms.Clear();
    }

    /// <inheritdoc />
    public void DeleteById(int id)
    {
        for (int i = 0; i < _gameRooms.Count; i++)
        {
            if (_gameRooms[i].Id.Equals(id))
                _gameRooms.RemoveAt(i);
        }
    }

    /// <inheritdoc />
    public IEnumerable<GameRoom> GetById(int id)
    {
        var gameRoom = _gameRooms.Where(x => x.Id == id);
        
        return gameRoom;
    }
}