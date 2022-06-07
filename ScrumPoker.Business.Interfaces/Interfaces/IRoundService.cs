using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IRoundService
{
    Round GetById(int id);
    void Update(Round round);
    List<VoteRegistration> GetHistory(int roundId);
}