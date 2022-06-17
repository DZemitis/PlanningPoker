using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

/// <inheritdoc />
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

    public Round Create(Round roundRequest)
    {
        var round = _roundRepository.Create(roundRequest);

        return round;
    }

    public void SetState(Round round)
    {
        _roundRepository.SetState(round);
    }

    public void Update(Round roundRequest)
    {
        _roundRepository.Update(roundRequest);
    }
}