namespace ScrumPoker.Business.Models.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<GameRoom> GameRooms { get; set; }
}