using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.EFContext;

public interface IScrumPokerContext
{
    DbSet<PlayerDto> Players { get; set; }
    DbSet<GameRoomDto> GameRooms { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync();
}