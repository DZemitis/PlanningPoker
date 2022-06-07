using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.ContextExtensions;

public class VotesExtension
{
    public static void Votes(ModelBuilder builder)
    {
        builder.Entity<VoteRegistrationDto>()
            .HasKey(v => v.Id);
        
        builder.Entity<VoteRegistrationDto>()
            .ToTable("Votes");

        builder.Entity<VoteRegistrationDto>()
            .HasIndex(v => v.RoundId);

        builder.Entity<VoteRegistrationDto>()
            .HasOne(r=>r.Round)
            .WithMany(r => r.Votes)
            .HasForeignKey(v => v.RoundId);

        builder.Entity<VoteRegistrationDto>()
            .HasOne(p => p.Player)
            .WithMany()
            .HasForeignKey(v => v.PlayerId);
    }
}