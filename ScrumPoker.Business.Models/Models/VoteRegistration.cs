namespace ScrumPoker.Business.Models.Models;

public class VoteRegistration
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int GameRoomId { get; set; }
    public int Vote { get; set; }
    public int RoundId { get; set; }
}