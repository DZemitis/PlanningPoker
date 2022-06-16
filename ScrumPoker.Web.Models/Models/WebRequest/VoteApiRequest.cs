namespace ScrumPoker.Web.Models.Models.WebRequest;

public class VoteApiRequest
{
    public int PlayerId { get; set; }
    public int VoteResult { get; set; }
    public int RoundId { get; set; }
}