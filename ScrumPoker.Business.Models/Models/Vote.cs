namespace ScrumPoker.Business.Models.Models;

public class Vote
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int VoteResult { get; set; }
    public int RoundId { get; set; }
}