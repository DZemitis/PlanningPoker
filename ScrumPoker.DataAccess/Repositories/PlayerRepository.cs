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

    public IEnumerable<Player> GetAll()
    {
        var players = Context.Players
            .Include(p => p.PlayerGameRooms).ThenInclude(grp => grp.GameRoom);
        var playerListResponse = Mapper.Map<List<Player>>(players);

        return playerListResponse;
    }

    public Player GetById(int id)
    {
        var playerDto = GetPlayerById(id);

        var playerDtoResponse = Mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }

    public Player Create(Player createPlayerRequest)
    {
        var addPlayer = new PlayerDto
        {
            Name = createPlayerRequest.Name,
            Email = createPlayerRequest.Email
        };

        Context.Players.Add(addPlayer);
        Context.SaveChanges();
        var playerDtoResponse = Mapper.Map<Player>(addPlayer);

        return playerDtoResponse;
    }

    public Player Update(Player updatePlayerRequest)
    {
        var playerDto = GetPlayerById(updatePlayerRequest.Id);

        playerDto.Name = updatePlayerRequest.Name;
        Context.SaveChanges();

        var playerDtoResponse = Mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }

    public void DeleteById(int id)
    {
        var playerDto = GetPlayerById(id);
        Context.Players.Remove(playerDto);
        Context.SaveChanges();
    }
}