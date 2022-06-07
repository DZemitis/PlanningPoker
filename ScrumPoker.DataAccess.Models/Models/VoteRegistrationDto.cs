namespace ScrumPoker.DataAccess.Models.Models;

public class VoteRegistrationDto
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public PlayerDto Player { get; set; } = null!;
    public int Vote { get; set; }
    public int RoundId { get; set; }
    public RoundDto Round { get; set; } = null!;
}