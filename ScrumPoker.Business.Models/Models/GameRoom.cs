namespace ScrumPoker.Business.Models.Models;

public class GameRoom
{
    public int Id { get; init; }
    public string Name { get; set; }
    public List<Player> Players { get; set; }
}