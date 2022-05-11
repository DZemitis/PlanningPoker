using ScrumPoker.DataAcces.Models.Models;

namespace ScrumPoker.Data.PersistenceMock;

public static class TempDb
{
    public static readonly List<GameRoomDto> GameRooms = new();
    public static readonly List<PlayerDto> PlayerList = new();
}