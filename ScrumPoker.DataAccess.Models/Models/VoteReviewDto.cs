namespace ScrumPoker.DataAccess.Models.Models;

public class VoteReviewDto
{
    public int Id { get; set; }
    public int VoteRegistrationId { get; set; }
    public string? Review { get; set; }
}