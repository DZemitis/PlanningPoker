using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data.ContextExtensions;

public class GameRoomPlayerContextExtension
{
    public static void GameRoomPlayer(ModelBuilder builder)
    {
        builder.Entity<GameRoomPlayer>()
            .HasKey(gp => new {gp.GameRoomId, gp.PlayerId});

        builder.Entity<GameRoomPlayer>()
            .HasOne(gr => gr.GameRoom)
            .WithMany(p => p.Players)
            .HasForeignKey(gr => gr.GameRoomId);

        builder.Entity<GameRoomPlayer>()
            .HasOne(p => p.Player)
            .WithMany(gr => gr.GameRooms)
            .HasForeignKey(p => p.PlayerId);

        builder.Entity<GameRoomPlayer>()
            .HasIndex(gp => new {gp.GameRoomId, gp.PlayerId});

        builder.Entity<GameRoomPlayer>()
            .ToTable("GameRoomsPlayers");
    }
}