using ScrumPoker.DataAcces.Models.Models;

namespace ScrumPoker.Data.Data;

public static class GameRepository
{
    public static List<GameRoomDto> _gameRooms = new List<GameRoomDto>();
    public static List<PlayerDto> _playerList = new List<PlayerDto>();
}