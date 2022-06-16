using ScrumPoker.Common.Models;

namespace ScrumPoker.Business.Models.Models;

public class Round
{
    public int RoundId { get; set; }
    public int GameRoomId { get; set; }
    public RoundState RoundState { get; set; }
    public string Description { get; set; } = null!;
    public List<Vote> Votes { get; set; } = null!;
}