namespace ScrumPoker.DataAccess.Models.Models;

public class RoundDto
{
    public int RoundId { get; set; }
    public Enum RoundState { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<VoteRegistrationDto> Votes { get; set; } = null!;
}