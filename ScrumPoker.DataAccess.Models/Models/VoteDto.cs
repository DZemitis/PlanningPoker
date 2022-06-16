namespace ScrumPoker.DataAccess.Models.Models;

public class VoteDto
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public PlayerDto Player { get; set; } = null!;
    public int VoteResult { get; set; }
    public int RoundId { get; set; }
    public RoundDto Round { get; set; } = null!;
}