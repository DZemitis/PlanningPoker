namespace ScrumPoker.DataAccess.Models.Models;

public class GameRoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<GameRoomPlayer> GameRoomPlayers { get; set; } = null!;
}