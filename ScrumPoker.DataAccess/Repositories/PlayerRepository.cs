using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Repositories;

/// <inheritdoc cref="ScrumPoker.DataAccess.Interfaces.IPlayerRepository" />
public class PlayerRepository : RepositoryBase, IPlayerRepository
{
    public PlayerRepository(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger) : base(mapper,
        context, logger)
    {
    }

    public async Task<IEnumerable<Player>> GetAll()
    {
        var players = await Context.Players
            .Include(p => p.PlayerGameRooms)
            .ThenInclude(grp => grp.GameRoom).ToListAsync();

        var playerListResponse = Mapper.Map<List<Player>>(players);

        return playerListResponse;
    }

    public async Task<Player> GetById(int id)
    {
        var playerDto = await GetPlayerById(id);

        var playerDtoResponse = Mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }

    public async Task<Player> Create(Player createPlayerRequest)
    {
        var addPlayer = new PlayerDto
        {
            Name = createPlayerRequest.Name,
            Email = createPlayerRequest.Email
        };

        await Context.Players.AddAsync(addPlayer);
        await Context.SaveChangesAsync();
        var playerDtoResponse = Mapper.Map<Player>(addPlayer);

        return playerDtoResponse;
    }

    public async Task<Player> Update(Player updatePlayerRequest)
    {
        var playerDto = await GetPlayerById(updatePlayerRequest.Id);

        playerDto.Name = updatePlayerRequest.Name;
        await Context.SaveChangesAsync();

        var playerDtoResponse = Mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }

    public async Task DeleteById(int id)
    {
        var playerDto = await GetPlayerById(id);
        Context.Players.Remove(playerDto);
        await Context.SaveChangesAsync();
    }
}