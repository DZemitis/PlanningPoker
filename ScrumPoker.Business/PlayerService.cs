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

    public IEnumerable<Player> GetAll()
    {
        return _playerRepository.GetAll();
    }

    public Player GetById(int id)
    {
        return _playerRepository.GetById(id);
    }

    public Player Create(Player createPlayerRequest)
    {
        return _playerRepository.Create(createPlayerRequest);
    }

    public Player Update(Player updatePlayerRequest)
    {
        return _playerRepository.Update(updatePlayerRequest);
    }

    public void DeleteById(int id)
    {
        _playerRepository.DeleteById(id);
    }
}