using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Business;

/// <inheritdoc />
public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUserManager _userManager;

    public PlayerService(IPlayerRepository playerRepository, IUserManager userManager)
    {
        _playerRepository = playerRepository;
        _userManager = userManager;
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
        var currentUserId = _userManager.GetCurrentUserId();
        
        updatePlayerRequest.Id = currentUserId;

        return await _playerRepository.Update(updatePlayerRequest);
    }

    public async Task DeleteById(int id)
    {
        await _playerRepository.DeleteById(id);
    }
}