namespace ScrumPoker.Business.Models.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<GameRoom> GameRooms { get; set; } = null!;
}