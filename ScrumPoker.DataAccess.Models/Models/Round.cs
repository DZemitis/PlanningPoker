using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ScrumPoker.DataAccess.Models.Models;

public class Round
{
    public int RoundId { get; set; }
    public Enum RoundState { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<VoteRegistration>? Votes { get; set; }
}