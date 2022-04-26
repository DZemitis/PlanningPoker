using AutoMapper;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAcces.Models.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Data.Data;

public class PlayerRepository : IPlayerRepository
{
    public PlayerRepository(IMapper mapper)
    {
        _mapper = mapper;
    }

    private static List<PlayerDto> _playerList = new List<PlayerDto>();
    private static int _id { get; set; }
    private readonly IMapper _mapper;
    
    public Player Create(Player createPlayerRequest)
    {
        var addPlayer = new PlayerDto
        {
            Name = createPlayerRequest.Name,
            Email = createPlayerRequest.Email,
            Id = ++_id
        };
        
        _playerList.Add(addPlayer);
        var playerDtoResponse = _mapper.Map<Player>(addPlayer);

        return playerDtoResponse;
    }

    public Player GetById(int id)
    {
        var playerDto = _playerList.FirstOrDefault(x => x.Id == id);
        var playerDtoResponse = _mapper.Map<Player>(playerDto);
        
        return playerDtoResponse;
    }

    public Player Update(Player updatePlayerRequest)
    {
        var playerDto = _playerList.FirstOrDefault(x => x.Id == updatePlayerRequest.Id);
        playerDto.Name = updatePlayerRequest.Name;
        playerDto.Email = updatePlayerRequest.Email;

        var playerDtoResponse = _mapper.Map<Player>(playerDto);

        return playerDtoResponse;
    }

    public List<Player> GetAll()
    {
        var playerListResponse = new List<Player>();
        foreach (var playerDto in _playerList)
        {
            var player = _mapper.Map<Player>(playerDto);
            playerListResponse.Add(player);
        }
        
        return playerListResponse;
    }

    public void DeleteById(int id)
    {
        for (int i = 0; i < _playerList.Count; i++)
        {
            if (_playerList[i].Id.Equals(id))
                _playerList.RemoveAt(i);
        }
    }

    public void UpdateGameRoomList(Player playerToUpdateRequest)
    {
        var playerToUpdateDto = _mapper.Map<PlayerDto>(playerToUpdateRequest);
        var playerDto = _playerList.FirstOrDefault(x => x.Id == playerToUpdateRequest.Id);
        
        playerDto.GameRooms = playerToUpdateDto.GameRooms;
    }
}