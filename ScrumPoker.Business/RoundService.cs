using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

public class RoundService : IRoundService
{
    private readonly IRoundRepository _roundRepository;

    public RoundService(IRoundRepository roundRepository)
    {
        _roundRepository = roundRepository;
    }

    public Round GetById(int id)
    {
        var round = _roundRepository.GetById(id);
        return round;
    }

    public void Update(Round round)
    {
        _roundRepository.Update(round);
    }

    public List<VoteRegistration> GetHistory(int roundId)
    {
       return _roundRepository.GetHistory(roundId);
    }
}