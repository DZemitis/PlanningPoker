using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.ContextExtensions;

public class VotesExtension
{
    public static void Votes(ModelBuilder builder)
    {
        builder.Entity<VoteDto>()
            .HasKey(v => v.Id);

        builder.Entity<VoteDto>()
            .ToTable("Votes");

        builder.Entity<VoteDto>()
            .HasIndex(v => v.RoundId);

        builder.Entity<VoteDto>()
            .HasOne(r => r.Round)
            .WithMany(r => r.Votes)
            .HasForeignKey(v => v.RoundId);

        builder.Entity<VoteDto>()
            .HasOne(p => p.Player)
            .WithMany()
            .HasForeignKey(v => v.PlayerId);
    }
}