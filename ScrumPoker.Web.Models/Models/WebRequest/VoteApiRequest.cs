namespace ScrumPoker.Web.Models.Models.WebRequest;

public class VoteApiRequest
{
    public int PlayerId { get; set; }
    public int Vote { get; set; }
    public int RoundId { get; set; }
    public int GameRoomId { get; set; }
}