using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

public interface IScrumPokerContext
{
    DbSet<T> Set<T>() where T : class;
    EntityEntry<T> Entry<T>(T entity) where T : class;
    DbSet<PlayerDto> Players { get; set; }
    DbSet<GameRoomDto> GameRooms { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync();
}