using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

/// <inheritdoc cref="ScrumPoker.DataAccess.Interfaces.IPlayerRepository" />
public class PlayerRepository : RepositoryBase ,IPlayerRepository
{
    public PlayerRepository(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger) : base(mapper, context, logger)
    {
    }

    public IEnumerable<Player> GetAll()
    {
        var Players = _context.Players
            .Include(p => p.PlayerGameRooms).ThenInclude(grp=>grp.GameRoom);
        var playerListResponse = _mapper.Map<List<Player>>(Players);
        
        return playerListResponse;
    }

    public Player GetById(int id)
    {
        var playerDto = PlayerIdValidation(id);

        var playerDtoResponse = _mapper.Map<Player>(playerDto);
        
        return playerDtoResponse;
    }

    public Player Create(Player createPlayerRequest)
    {
        ValidateAlreadyExistPlayer(createPlayerRequest);
        
        var addPlayer = new PlayerDto
        {
            Name = createPlayerRequest.Name,
            Email = createPlayerRequest.Email
        };
        
        _context.Players.Add(addPlayer);
        _context.SaveChanges();
        var playerDtoResponse = _mapper.Map<Player>(addPlayer);

        return playerDtoResponse;
    }

    public Player Update(Player updatePlayerRequest)
    {
        
        var playerDto = PlayerIdValidation(updatePlayerRequest.Id);

        playerDto.Name = updatePlayerRequest.Name;
        _context.SaveChanges();

        var playerDtoResponse = _mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }


    public void DeleteById(int id)
    {
        var playerDto = PlayerIdValidation(id);
        _context.Players.Remove(playerDto);
        _context.SaveChanges();
    }
}