namespace ScrumPoker.Web.Models.Models.WebResponse;

public class ScrumPokerError
{
    public string? Field { get; set; }
    public List<string>? Messages { get; set; }
}