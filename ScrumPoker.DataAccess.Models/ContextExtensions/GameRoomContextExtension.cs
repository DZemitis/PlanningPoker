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
    }
}