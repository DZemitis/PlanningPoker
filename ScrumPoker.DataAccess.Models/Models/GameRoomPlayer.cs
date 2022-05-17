namespace ScrumPoker.DataAccess.Models.Models;

public class GameRoomPlayer
{
    public int GameRoomId { get; set; }
    public GameRoomDto GameRoom { get; set; } = null!;
    public int PlayerId { get; set; }
    public PlayerDto Player { get; set; } = null!;
}