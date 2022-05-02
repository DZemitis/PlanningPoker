using ScrumPoker.DataAcces.Models.Models;

namespace ScrumPoker.Data.PersistenceMock;

public static class TempDb
{
    public static readonly List<GameRoomDto> _gameRooms = new List<GameRoomDto>();
    public static readonly List<PlayerDto> _playerList = new List<PlayerDto>();
}