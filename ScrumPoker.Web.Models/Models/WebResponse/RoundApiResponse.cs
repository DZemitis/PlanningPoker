using ScrumPoker.Common.Models;

namespace ScrumPoker.Web.Models.Models.WebResponse;

public class RoundApiResponse
{
    public int RoundId { get; set; }
    public int GameRoomId { get; set; }
    public RoundState RoundState { get; set; }
    public string Description { get; set; } = null!;
    public List<VoteInRoundApiResponse> Votes { get; set; } = null!;
}