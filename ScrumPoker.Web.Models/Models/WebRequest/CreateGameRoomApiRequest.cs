namespace ScrumPoker.Web.Models.Models.WebRequest;

public class CreateGameRoomApiRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}