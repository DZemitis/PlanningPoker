using ScrumPoker.Core.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Data.Data;

/// <inheritdoc />
public class GameRoomRepository : IGameRoomRepository
{
    private static readonly List<GameRoom> _gameRooms = new List<GameRoom>();
    private static int Id { get; set; }
    
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
    
    public List<GameRoom> GetAll()
    {
        return _gameRooms;
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
        var gameRoom = _gameRooms.FirstOrDefault(x => x.Id == gameRoomRequest.Id);
        gameRoom.Name = gameRoomRequest.Name;

        return gameRoom;
    }

    public void DeleteAll()
    {
        _gameRooms.Clear();
    }

    public void DeleteById(int id)
    {
        for (int i = 0; i < _gameRooms.Count; i++)
        {
            if (_gameRooms[i].Id.Equals(id))
                _gameRooms.RemoveAt(i);
        }
    }

    public GameRoom GetById(int id)
    {
        var gameRoom = _gameRooms.FirstOrDefault(x => x.Id == id);
        
        return gameRoom;
    }
}