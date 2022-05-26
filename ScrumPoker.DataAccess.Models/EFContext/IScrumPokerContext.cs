using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Models.EFContext;

public interface IScrumPokerContext
{
    DbSet<PlayerDto> Players { get; set; }
    DbSet<GameRoomDto> GameRooms { get; set; }
    DbSet<RoundDto> Rounds { get; set; }
    DbSet<VoteRegistrationDto> Votes { get; set; }
    DbSet<VoteReviewDto> VoteReviews { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync();
}