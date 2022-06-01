using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

/// <inheritdoc />
public class PlayerRepository : IPlayerRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly IValidation _validator;
    private readonly IValidation _validator;

    public PlayerRepository(IMapper mapper, IScrumPokerContext context, ILogger<PlayerRepository> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _validator = validator;
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
        var playerDto = _validator.PlayerIdValidation(id);

        var playerDtoResponse = _mapper.Map<Player>(playerDto);
        
        return playerDtoResponse;
    }

    public Player Create(Player createPlayerRequest)
    {
        _validator.ValidateAlreadyExistPlayer(createPlayerRequest);
        
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
        
        var playerDto = _validator.PlayerIdValidation(updatePlayerRequest.Id);

        playerDto.Name = updatePlayerRequest.Name;
        _context.SaveChanges();

        var playerDtoResponse = _mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }

    public void DeleteById(int id)
    {
        var playerDto = _validator.PlayerIdValidation(id);
        _context.Players.Remove(playerDto);
        _context.SaveChanges();
    }
}