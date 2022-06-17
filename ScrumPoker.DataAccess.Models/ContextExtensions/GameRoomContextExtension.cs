using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.ContextExtensions;

public class GameRoomContextExtension
{
    public static void GameRoom(ModelBuilder builder)
    {
        builder.Entity<GameRoomDto>()
            .HasKey(gr => gr.Id);

        builder.Entity<GameRoomDto>()
            .HasIndex(gr => gr.Id);

        builder.Entity<GameRoomDto>()
            .ToTable("GameRooms");

        builder.Entity<GameRoomDto>()
            .Property(x => x.CurrentRoundId)
            .IsRequired(false);

        builder.Entity<GameRoomDto>()
            .Property(x => x.MasterId)
            .IsRequired(false);

        builder.Entity<GameRoomDto>()
            .HasOne(g => g.CurrentRound)
            .WithOne()
            .HasForeignKey<GameRoomDto>(g => g.CurrentRoundId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<GameRoomDto>()
            .HasOne(x => x.Master)
            .WithMany()
            .HasForeignKey(x => x.MasterId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}