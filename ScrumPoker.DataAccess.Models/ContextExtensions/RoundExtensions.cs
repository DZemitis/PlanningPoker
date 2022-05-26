using Microsoft.EntityFrameworkCore;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.ContextExtensions;

public class RoundExtensions
{
    public static void Round(ModelBuilder builder)
    {
        builder.Entity<RoundDto>()
            .HasKey(r => r.RoundId);

        builder.Entity<RoundDto>()
            .HasIndex(r => new { r.RoundId });

        builder.Entity<RoundDto>()
            .ToTable("Rounds");
    }
}