namespace ScrumPoker.Web.Models.Models.WebRequest;

public class UpdatePlayerApiRequest
{
    public int Id { get; set; }
    public string Opinion { get; set; }
    public int Vote { get; set; }
}