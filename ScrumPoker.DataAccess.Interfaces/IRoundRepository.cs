using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IRoundRepository
{
    void SetRoundState(Round round);
    void SetRound(Round round);
}