using ScrumPoker.DataAcces.Models.Models;

namespace ScrumPoker.Data.PersistenceMock;

public static class TempDb
{
    public static List<GameRoomDto> _gameRooms = new List<GameRoomDto>();
    public static List<PlayerDto> _playerList = new List<PlayerDto>();
}