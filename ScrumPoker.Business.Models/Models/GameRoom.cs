namespace ScrumPoker.Business.Models.Models;

public class GameRoom
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public List<Player> Players { get; set; } = null!;
    public int MasterId { get; set; }
    public int CurrentRoundId { get; set; }
    public Round Round { get; set; } = null!;
    public List<Round> Rounds { get; set; } = null!;
}