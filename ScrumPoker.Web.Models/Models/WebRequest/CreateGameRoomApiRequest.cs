namespace ScrumPoker.Web.Models.Models.WebRequest;

public class CreateGameRoomApiRequest
{
    public string? Name { get; set; }
    public string Story { get; set; } = null!;
    public int MasterId { get; set; }
}