namespace ScrumPoker.Web.Models.Models.WebRequest;

public class CreateRoundApiRequest
{
    public int GameRoomId { get; set; }
    public string Description { get; set; } = null!;
}