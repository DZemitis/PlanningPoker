using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Common.Models;

namespace ScrumPoker.Business;

public class RoundStateService : IRoundStateService
{
    private static readonly IReadOnlyList<RoundState> Grooming = new List<RoundState>()
    {
        RoundState.VoteRegistration
    };
    
    private static readonly IReadOnlyList<RoundState> VoteRegistration = new List<RoundState>()
    {
        RoundState.VoteReview
    };
    
    private static readonly IReadOnlyList<RoundState> VoteReview = new List<RoundState>()
    {
        RoundState.VoteRegistration,
        RoundState.Finished
    };
    
    private static readonly IReadOnlyList<RoundState> Finished = new List<RoundState>()
    {
    };
}