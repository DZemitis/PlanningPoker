namespace ScrumPoker.DataAccess.Models.Models;

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<GameRoomPlayer> PlayerGameRooms { get; set; } = null!;
    public VoteDto? PLayersVote { get; set; }
}