using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.EFContext;

public interface IScrumPokerContext
{
    DbSet<PlayerDto> Players { get; set; }
    DbSet<GameRoomDto> GameRooms { get; set; }
    DbSet<RoundDto> Rounds { get; set; }
    DbSet<VoteDto> Votes { get; set; }
    DbSet<GameRoomPlayer> GameRoomsPlayers { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync();
}