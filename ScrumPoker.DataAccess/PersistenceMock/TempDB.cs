using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.PersistenceMock;

public static class TempDb
{
    public static readonly List<GameRoomDto> GameRooms = new();
    public static readonly List<PlayerDto> PlayerList = new();
}