using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.Models;

namespace ScrumPoker.Business;

public class RoundStateService : IRoundStateService
{
    private static readonly IReadOnlyList<RoundState> Grooming = new List<RoundState>
    {
        RoundState.VoteRegistration
    };
    
    private static readonly IReadOnlyList<RoundState> VoteRegistration = new List<RoundState>
    {
        RoundState.VoteReview
    };
    
    private static readonly IReadOnlyList<RoundState> VoteReview = new List<RoundState>
    {
        RoundState.VoteRegistration,
        RoundState.Finished
    };

    private static readonly IReadOnlyList<RoundState> Finished = new List<RoundState>();
    
    private static IReadOnlyList<RoundState> CheckRoundStates(Round roundRequest)
    {
        var result = roundRequest.RoundState switch
        {
            RoundState.Grooming => Grooming,
            RoundState.VoteRegistration => VoteRegistration,
            RoundState.VoteReview => VoteReview,
            RoundState.Finished => Finished,
            _ => throw new ArgumentOutOfRangeException()
        };
        
       return result;
    }
    
    public void ValidateRoundState(Round roundRequest, Round roundDto)
    {
        if (!CheckRoundStates(roundDto).Contains(roundRequest.RoundState))
            throw new InvalidRoundStateException(
                $"Round state {roundRequest.RoundState.ToString()} is not allowed after {roundDto.RoundState.ToString()}");
    }
}