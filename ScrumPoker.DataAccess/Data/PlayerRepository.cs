using AutoMapper;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.Data.PersistenceMock;
using ScrumPoker.DataAcces.Models.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Data.Data;

/// <inheritdoc />
public class PlayerRepository : IPlayerRepository
{
    private static int _id { get; set; }
    private readonly IMapper _mapper;

    public PlayerRepository(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<Player> GetAll()
    {
        var playerListResponse = _mapper.Map<List<Player>>(TempDb._playerList);
        
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
            Email = createPlayerRequest.Email,
            Id = ++_id
        };
        
        TempDb.PlayerList.Add(addPlayer);
        var playerDtoResponse = _mapper.Map<Player>(addPlayer);

        return playerDtoResponse;
    }

    public Player Update(Player updatePlayerRequest)
    {
        
        var playerDto = PlayerIdValidation(updatePlayerRequest.Id);

        playerDto.Name = updatePlayerRequest.Name;

        var playerDtoResponse = _mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }

    public void DeleteById(int id)
    {
        PlayerIdValidation(id);
        
        TempDb.PlayerList.RemoveAll(x => x.Id == id);
    }

    private static void ValidateAlreadyExist(Player player)
    {
        if (TempDb.GameRooms.Any(x => x.Id == player.Id))
        {
            throw new IdAlreadyExistException($"{typeof(Player)} with {player.Id} already exist");
        }
    }
    
    private static PlayerDto PlayerIdValidation(int playerId)
    {
        var playerDto = TempDb.PlayerList.SingleOrDefault(x => x.Id == playerId);

        if (playerDto == null)
        {
            throw new IdNotFoundException($"{typeof(Player)} with ID {playerId} not found");
        }

        return playerDto;
    }
}