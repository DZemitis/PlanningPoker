namespace ScrumPoker.Web.Models.Models.WebResponse;

public class GameRoomAddPlayerApiResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<PlayerInGameRoomApiResponse>? Players { get; set; }
}