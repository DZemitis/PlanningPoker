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
            .HasIndex(v => new {v.Id, v.GameRoomId, v.RoundId});

        builder.Entity<VoteRegistrationDto>()
            .ToTable("Votes");
    }
}