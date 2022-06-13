using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IRoundService
{
    Round GetById(int id);
    void SetState(Round round);
    List<VoteRegistration> GetHistory(int roundId);
    void Update(Round roundRequest);
}