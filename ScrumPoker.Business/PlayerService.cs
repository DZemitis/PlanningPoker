using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

/// <inheritdoc />
public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<IEnumerable<Player>> GetAll()
    {
        return await _playerRepository.GetAll();
    }

    public async Task<Player> GetById(int id)
    {
        return await _playerRepository.GetById(id);
    }

    public async Task<Player> Create(Player createPlayerRequest)
    {
        return await _playerRepository.Create(createPlayerRequest);
    }

    public async Task<Player> Update(Player updatePlayerRequest)
    {
        return await _playerRepository.Update(updatePlayerRequest);
    }

    public async Task DeleteById(int id)
    {
        await _playerRepository.DeleteById(id);
    }
}