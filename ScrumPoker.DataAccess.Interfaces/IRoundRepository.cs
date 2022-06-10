using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IRoundRepository
{
    Round GetById(int id);
    void Update(Round round);
    List<VoteRegistration> GetHistory(int roundId);
}