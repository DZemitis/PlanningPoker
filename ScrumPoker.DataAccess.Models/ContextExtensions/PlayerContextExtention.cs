using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.ContextExtensions;

public class PlayerContextExtension
{
    public static void Player(ModelBuilder builder)
    {
        builder.Entity<PlayerDto>()
            .HasKey(p => p.Id);

        builder.Entity<PlayerDto>()
            .HasIndex(p => p.Id);

        builder.Entity<PlayerDto>()
            .ToTable("Players");
    }
}