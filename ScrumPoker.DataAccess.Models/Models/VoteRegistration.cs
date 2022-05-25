namespace ScrumPoker.DataAccess.Models.Models;

public class VoteRegistration
{
    public int PlayerId { get; set; }
    public int GameRoomId { get; set; }
    public int Vote { get; set; }
}