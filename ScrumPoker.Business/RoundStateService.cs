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

    public void ValidateRoundState(RoundState nextState, RoundState currentState)
    {
        if (!GetAvailableTransitionStates(currentState).Contains(nextState))
        {
            throw new InvalidRoundStateException(
                $"Round state {nextState.ToString()} is not allowed after {currentState.ToString()}");
        }
    }

    private static IEnumerable<RoundState> GetAvailableTransitionStates(RoundState roundRequest)
    {
        var result = roundRequest switch
        {
            RoundState.Grooming => Grooming,
            RoundState.VoteRegistration => VoteRegistration,
            RoundState.VoteReview => VoteReview,
            RoundState.Finished => Finished,
            _ => throw new ArgumentOutOfRangeException()
        };
        
       return result;
    }
}