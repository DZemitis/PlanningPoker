namespace ScrumPoker.Business.Models.Models;

public class GameRoom
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public List<Player> Players { get; set; } = null!;
    public string Story { get; set; } = null!;
    public int MasterId { get; set; }
    public int? Round { get; set; }
    public int? RoundState { get; set; }
}