using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
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
        var PlayerDto = await GetById(updatePlayerRequest.Id);
        var playerId = _userManager.GetUserId();
        if (PlayerDto.Id != playerId)
            throw new HasNoClaimException($"User has not rights to Update player (ID {PlayerDto.Id})");

        return await _playerRepository.Update(updatePlayerRequest);
    }

    public async Task DeleteById(int id)
    {
        await _playerRepository.DeleteById(id);
    }
}