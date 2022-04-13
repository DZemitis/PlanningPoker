using ScrumPoker.Core.Models;

namespace ScrumPoker.Core.Services;

public interface IGameRoomService
{
    void CreateGameRoom(GameRoom name);
    IEnumerable<GameRoom> GetGameRoomByName(string name);
    List<GameRoom> GetAllGameRooms();
    void DeleteAllGameRooms();
    void DeleteGameRoomByName(string name);
}