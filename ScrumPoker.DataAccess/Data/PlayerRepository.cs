using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

/// <inheritdoc />
public class PlayerRepository : IPlayerRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger<PlayerRepository> _logger;

    public PlayerRepository(IMapper mapper, IScrumPokerContext context, ILogger<PlayerRepository> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
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
        ValidateAlreadyExist(createPlayerRequest);
        
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

    private void ValidateAlreadyExist(Player player)
    {
        if (_context.Players.Any(p => p.Id == player.Id))
        {
            _logger.LogWarning("Player with ID{ID} already exists", player.Id);
            throw new IdAlreadyExistException($"{typeof(Player)} with {player.Id} already exist");
        }
    }
    
    private PlayerDto PlayerIdValidation(int playerId)
    {
        var playerDto = _context.Players
            .Include(p=>p.PlayerGameRooms).ThenInclude(grp=>grp.GameRoom)
            .SingleOrDefault(x => x.Id == playerId);

        if (playerDto == null)
        {
            _logger.LogWarning("Player with ID{ID} could not be found", playerId);
            throw new IdNotFoundException($"{typeof(Player)} with ID {playerId} not found");
        }

        return playerDto;
    }
}