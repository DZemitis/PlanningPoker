using System.Data;
using ScrumPoker.Core.Models;
using ScrumPoker.Core.Services;
using ScrumPoker.Data.Data;

namespace ScrumPoker.Services;

public class GameRoomService : IGameRoomService
{
    
    public void CreateGameRoom(GameRoom name)
    {
        DataBase.GameRooms.Add(name);
    }
    
    public IEnumerable<GameRoom> GetGameRoomByName(string name)
    {
        var gameRoom = DataBase.GameRooms.Where(x => x.Name == name);
        
        return gameRoom;
    }

    public List<GameRoom> GetAllGameRooms()
    {
        return DataBase.GameRooms;
    }

    public void DeleteAllGameRooms()
    {
        DataBase.GameRooms.Clear();
    }

    public void DeleteGameRoomByName(string name)
    {
        for (int i = 0; i < DataBase.GameRooms.Count; i++)
        {
            if (DataBase.GameRooms[i].Name.Equals(name))
                DataBase.GameRooms.RemoveAt(i);
        }
    }
}