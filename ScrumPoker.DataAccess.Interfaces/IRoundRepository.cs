using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IRoundRepository
{
    void SetRoundState(Round round);
    Round GetById(int id);
    void Update(Round round);
}