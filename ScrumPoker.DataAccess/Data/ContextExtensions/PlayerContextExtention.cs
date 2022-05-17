using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data.ContextExtensions;

public class PlayerContextExtension
{
    public static void Player(ModelBuilder builder)
    {
        builder.Entity<PlayerDto>()
            .HasKey(p => p.Id);

        builder.Entity<PlayerDto>()
            .HasIndex(p => new { p.Id, p.Email });

        builder.Entity<PlayerDto>()
            .ToTable("Players");
    }
}