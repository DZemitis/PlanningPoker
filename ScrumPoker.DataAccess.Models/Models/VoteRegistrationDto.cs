namespace ScrumPoker.DataAccess.Models.Models;

public class VoteRegistrationDto
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int GameRoomId { get; set; }
    public int Vote { get; set; }
    public int RoundId { get; set; }
}