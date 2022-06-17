namespace ScrumPoker.Web.Models.Models.WebResponse;

public class GameRoomAllApiResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<PlayerInGameRoomApiResponse>? Players { get; set; }
    public int MasterId { get; set; }
    public int CurrentRoundId { get; set; }
}