using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.ContextExtensions;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.EFContext;

public class ScrumPokerContext : DbContext, IScrumPokerContext
{
    public DbSet<GameRoomDto> GameRooms { get; set; } = null!;
    public DbSet<RoundDto> Rounds { get; set; } = null!;
    public DbSet<VoteRegistrationDto> Votes { get; set; } = null!;
    public DbSet<GameRoomPlayer> GameRoomsPlayers { get; set; } = null!;
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