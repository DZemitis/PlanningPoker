namespace ScrumPoker.Web.Models.Models.WebResponse;

public class GameRoomApiResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<PlayerInGameRoomApiResponse>? Players { get; set; }
    public int MasterId { get; set; }
    public int CurrentRoundId { get; set; }
    public List<RoundIdApiResponse> Rounds { get; set; } = null!;
}