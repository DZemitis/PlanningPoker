using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.ContextExtensions;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.EFContext;

public class ScrumPokerContext : DbContext, IScrumPokerContext
{
    public ScrumPokerContext(DbContextOptions<ScrumPokerContext> options) : base(options)
    {
    }

    public DbSet<GameRoomDto> GameRooms { get; set; } = null!;
    public DbSet<RoundDto> Rounds { get; set; } = null!;
    public DbSet<VoteDto> Votes { get; set; } = null!;
    public DbSet<GameRoomPlayer> GameRoomsPlayers { get; set; } = null!;
    public DbSet<PlayerDto> Players { get; set; } = null!;

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        GameRoomPlayerContextExtension.GameRoomPlayer(builder);
        GameRoomContextExtension.GameRoom(builder);
        PlayerContextExtension.Player(builder);
        RoundsExtension.Round(builder);
        VotesExtension.Votes(builder);
    }
}