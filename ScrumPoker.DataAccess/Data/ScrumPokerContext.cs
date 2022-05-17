using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Data.ContextExtensions;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

public class ScrumPokerContext : DbContext, IScrumPokerContext
{
    public DbSet<GameRoomDto> GameRooms { get; set; } = null!;
    public DbSet<PlayerDto> Players { get; set; } = null!;

    public ScrumPokerContext(DbContextOptions<ScrumPokerContext> options) : base(options)
    {
 
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        GameRoomPlayerContextExtension.GameRoomPlayer(builder);
        GameRoomContextExtension.GameRoom(builder);
        PlayerContextExtension.Player(builder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}