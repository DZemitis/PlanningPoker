namespace ScrumPoker.Web.Models.Models.WebRequest;

public class UpdateDescriptionRoundApiRequest
{
    public int RoundId { get; set; }
    public string Description { get; set; } = null!;
}