using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataBase.Interfaces;

namespace ScrumPoker.Services;

/// <inheritdoc />
public class GameRoomService : IGameRoomService
{
    private readonly IGameRoomRepository _gameRoomRepository;
    private readonly IPlayerRepository _playerRepository;

    public GameRoomService(IGameRoomRepository gameRoomRepository, IPlayerRepository playerRepository)
    {
        _gameRoomRepository = gameRoomRepository;
        _playerRepository = playerRepository;
    }

    public GameRoom Create(GameRoom gameRoomRequest)
    {
        var gameRoom =_gameRoomRepository.Create(gameRoomRequest);

        return gameRoom;
    }
    
    public GameRoom GetById(int id)
    {
        return _gameRoomRepository.GetById(id);
    }

    public List<GameRoom> GetAll()
    {
        return _gameRoomRepository.GetAll();
    }

    public GameRoom Update(GameRoom gameRoomRequest)
    {
       return _gameRoomRepository.Update(gameRoomRequest);
    }

    public void DeleteAll()
    {
        _gameRoomRepository.DeleteAll();
    }

    public void DeleteById(int id)
    {
        _gameRoomRepository.DeleteById(id);
    }

    public void AddPlayer(int gameRoomId, int playerId)
    {
        var gameRoomToAdd = _gameRoomRepository.GetById(gameRoomId);
        var playerToAdd = _playerRepository.GetById(playerId);
        gameRoomToAdd.Players.Add(playerToAdd);
        
        _gameRoomRepository.UpdatePlayerList(gameRoomToAdd);
    }

    public void RemovePlayer(int gameRoomId, int playerId)
    {
        var gameRoomToDeleteFrom = _gameRoomRepository.GetById(gameRoomId);
        var playerToRemove = _playerRepository.GetById(playerId);

        for (int i = 0; i < playerToRemove.GameRooms.Count; i++)
        {
            if(playerToRemove.GameRooms[i].Id.Equals(gameRoomToDeleteFrom.Id))
                playerToRemove.GameRooms.RemoveAt(i);
        }

        for (int i = 0; i < gameRoomToDeleteFrom.Players.Count; i++)
        {
            if(gameRoomToDeleteFrom.Players[i].Id.Equals(playerToRemove.Id))
                gameRoomToDeleteFrom.Players.RemoveAt(i);
        }

        _playerRepository.UpdateGameRoomList(playerToRemove);
        _gameRoomRepository.UpdatePlayerList(gameRoomToDeleteFrom);
    }
}