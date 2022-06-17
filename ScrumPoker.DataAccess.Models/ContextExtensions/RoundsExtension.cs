using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.ContextExtensions;

public class RoundsExtension
{
    public static void Round(ModelBuilder builder)
    {
        builder.Entity<RoundDto>()
            .HasKey(r => r.RoundId);

        builder.Entity<RoundDto>()
            .HasIndex(r => new {r.RoundId, r.GameRoomId});

        builder.Entity<RoundDto>()
            .ToTable("Rounds");

        builder.Entity<RoundDto>()
            .HasOne(x => x.GameRoom)
            .WithMany(x => x.Rounds)
            .OnDelete(DeleteBehavior.NoAction);
    }
}