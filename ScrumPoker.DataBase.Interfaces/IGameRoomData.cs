using ScrumPoker.Core.Models;

namespace ScrumPoker.DataBase.Interfaces;

public interface IGameRoomData
{
    void Create(string gameRoom);
    List<GameRoom> GetAllGameRooms();
    void DeleteAllRooms();
    void DeleteRoomByName(string name);
    IEnumerable<GameRoom> GetGameRoomByName(string name);
}