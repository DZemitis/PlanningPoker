using ScrumPoker.Common.Models;

namespace ScrumPoker.Web.Models.Models.WebRequest;

public class UpdateRoundApiRequest
{
    public int RoundId { get; set; }
    public RoundState RoundState { get; set; }
}