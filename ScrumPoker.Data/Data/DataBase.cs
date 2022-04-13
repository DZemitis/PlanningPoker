using ScrumPoker.Core.Models;

namespace ScrumPoker.Data.Data;

public static class DataBase
{
    public static List<Player> Players { get; set; }
    public static List<GameRoom> GameRooms { get; set; }
    public static List<VotingResults> VotingResults { get; set; }
}