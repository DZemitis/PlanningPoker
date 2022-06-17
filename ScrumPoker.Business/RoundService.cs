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

    public async Task<Round> GetById(int id)
    {
        var round = await _roundRepository.GetById(id);
        return round;
    }

    public async Task<Round> Create(Round roundRequest)
    {
        var round = await _roundRepository.Create(roundRequest);

        return round;
    }

    public async Task SetState(Round round)
    {
        await _roundRepository.SetState(round);
    }

    public async Task Update(Round roundRequest)
    {
        await _roundRepository.Update(roundRequest);
    }
}