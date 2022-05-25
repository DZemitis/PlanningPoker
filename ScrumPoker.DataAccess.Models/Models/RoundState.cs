using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ScrumPoker.DataAccess.Models.Models;

public class RoundState
{
    public int State { get; set; }
    public string Description { get; set; } = null!;
}