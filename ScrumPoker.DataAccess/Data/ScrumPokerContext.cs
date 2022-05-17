using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

public class ScrumPokerContext : DbContext, IScrumPokerContext
{
    public DbSet<GameRoomDto> GameRooms { get; set; } = null!;
    public DbSet<PlayerDto> Players { get; set; } = null!;

    public ScrumPokerContext(DbContextOptions<ScrumPokerContext> options) : base(options)
    {
 
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}