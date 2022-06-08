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
            .HasMany(x => x.Rounds)
            .WithOne(x => x.GameRoom)
            .HasForeignKey(x=>x.GameRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<GameRoomDto>()
            .HasOne(g => g.CurrentRound)
            .WithOne()
            .HasForeignKey<GameRoomDto>(g => g.CurrentRoundId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<GameRoomDto>()
            .HasOne(x => x.Master)
            .WithMany()
            .HasForeignKey(x => x.MasterId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}