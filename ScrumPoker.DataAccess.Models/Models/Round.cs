using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ScrumPoker.DataAccess.Models.Models;

public class Round
{
    public int RoundNr { get; set; }
    public int RoundState { get; set; }
    public string Description { get; set; } = null!;
    public List<VoteRegistration>? Votes { get; set; }
}