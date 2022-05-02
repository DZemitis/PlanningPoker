using AutoMapper;
using ScrumPoker.Business.Models.Models;
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
        var playerDto = TempDb._playerList.Single(x => x.Id == id);
        var playerDtoResponse = _mapper.Map<Player>(playerDto);
        
        return playerDtoResponse;
    }

    public Player Create(Player createPlayerRequest)
    {
        var addPlayer = new PlayerDto
        {
            Name = createPlayerRequest.Name,
            Email = createPlayerRequest.Email,
            Id = ++_id
        };
        
        TempDb._playerList.Add(addPlayer);
        var playerDtoResponse = _mapper.Map<Player>(addPlayer);

        return playerDtoResponse;
    }

    public Player Update(Player updatePlayerRequest)
    {
        var playerDto = TempDb._playerList.Single(x => x.Id == updatePlayerRequest.Id);
        playerDto.Name = updatePlayerRequest.Name;

        var playerDtoResponse = _mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }

    public void DeleteById(int id)
    {
        TempDb._playerList.RemoveAll(x => x.Id == id);
    }

    public void UpdateGameRoomList(Player playerToUpdateRequest)
    {
        var playerToUpdateDto = _mapper.Map<PlayerDto>(playerToUpdateRequest);
        var playerDto = TempDb._playerList.Single(x => x.Id == playerToUpdateRequest.Id);
        
        playerDto.GameRooms = playerToUpdateDto.GameRooms;
    }
}