using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ScrumPoker.Business;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ForbiddenExceptions;
using ScrumPoker.DataAccess.Interfaces;
using Xunit;

namespace ScrumPoker.Test;

public class GameRoomServiceTests
{
    private readonly GameRoomService _sut;
    private readonly Mock<IGameRoomRepository> _gameRoomRepoMock = new();
    private readonly Mock<IUserManager> _userManagerMock = new();

    private readonly GameRoom _gameRoom;
    private int _currentUserId = 2;
    private readonly List<GameRoom> _gameRoomList = new();

    public GameRoomServiceTests()
    {
        _sut = new GameRoomService(_gameRoomRepoMock.Object, _userManagerMock.Object);

        _gameRoom = new GameRoom
        {
            Id = 1,
            Name = "Game Room",
            MasterId = 2,
            CurrentRoundId = _currentUserId,
            Players = new List<Player>()
        };
        
        _gameRoomList.Add(_gameRoom);
        _gameRoomRepoMock.Setup(x => x.GetById(_gameRoom.Id))
            .ReturnsAsync(_gameRoom);
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _gameRoomRepoMock.Setup(x => x.AddPlayerToRoom(_gameRoom.Id, _currentUserId));
    }

    [Fact]
    public async Task GetAll_ShouldReturnListOfGameRooms_ShouldPass()
    {
        //Arrange
        _gameRoomRepoMock.Setup(x => x.GetAll()).ReturnsAsync(_gameRoomList);

        //Act
        var gameRoomList = await _sut.GetAll();

        //Assert
        gameRoomList.Should().Contain(_gameRoom);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnGameRoom_WhenExist()
    {
        //Act
        var gameRoom = await _sut.GetById(1);

        //Assert
        gameRoom.Should().Be(_gameRoom);
    }

    [Fact]
    public async Task GetById_ShouldReturnNothing_WhenDoesNotExist()
    {
        //Arrange
        _gameRoomRepoMock.Setup(x => x.GetById(2))
            .ReturnsAsync(() => null!);

        //Act
        var gameRoom = await _sut.GetById(2);

        //Assert
        gameRoom.Should().BeNull();
    }

    [Fact]
    public async Task Update_ShouldUpdateRoundName_ShouldPass()
    {
        //Arrange
        var gameRoomUpdateRequest = new GameRoom {Id = 1, Name = "new name"};
        _gameRoomRepoMock.Setup(x => x.GetById(gameRoomUpdateRequest.Id))
            .ReturnsAsync(_gameRoom);

        //Act
        await _sut.Update(gameRoomUpdateRequest);

        //Assert
        _gameRoomRepoMock.Verify(x => x.Update(gameRoomUpdateRequest), Times.Once);
    }

    [Fact]
    public async Task Update_ShouldThrowException_WhenUserNotMaster()
    {
        //Arrange
        _currentUserId = 9;
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        var gameRoomUpdateRequest = new GameRoom {Id = 1, Name = "new name"};
        _gameRoomRepoMock.Setup(x => x.GetById(gameRoomUpdateRequest.Id))
            .ReturnsAsync(_gameRoom);

        //Act
        var action = () => _sut.Update(gameRoomUpdateRequest);

        //Assert
        await action.Should().ThrowAsync<ActionNotAllowedException>()
            .WithMessage($"User has not rights to Update game room (ID {_gameRoom.Id})");
    }

    [Fact]
    public async Task Create_ShouldCreateNewRound_ShouldPass()
    {
        //Arrange
        var gameRoomCreateRequest = new GameRoom {Name = "Game Room"};
        _gameRoomRepoMock.Setup(x => x.Create(gameRoomCreateRequest))
            .ReturnsAsync(_gameRoom);


        //Act
        var gameRoom = await _sut.Create(gameRoomCreateRequest);

        //Assert
        gameRoom.Should().Be(_gameRoom);
    }

    [Fact]
    public async Task DeleteAll_ShouldTriggerDeleteAll_ShouldPass()
    {
        //Act
        _gameRoomRepoMock.Setup(x => x.DeleteAll());

        //Act
        await _sut.DeleteAll();

        //Assert
        _gameRoomRepoMock.Verify(x => x.DeleteAll(), Times.Once);
    }

    [Fact]
    public async Task DeleteById_ShouldTriggerDeleteById_ShouldPass()
    {
        //Act
        _gameRoomRepoMock.Setup(x => x.DeleteById(_gameRoom.Id));

        //Act
        await _sut.DeleteById(_gameRoom.Id);

        //Assert
        _gameRoomRepoMock.Verify(x => x.DeleteById(_gameRoom.Id), Times.Once);
    }

    [Fact]
    public async Task AddPlayer_ShouldTriggerAddPlayerToRoom_ShouldPass()
    {
        //Arrange
        const int PlayerId = 3;

        //Act
        await _sut.AddPlayer(_gameRoom.Id, PlayerId);

        //Assert
        _gameRoomRepoMock.Verify(x => x.AddPlayerToRoom(_gameRoom.Id, PlayerId), Times.Once);
    }

    [Fact]
    public async Task AddPlayer_ShouldThrowException_WhenUserNotMaster()
    {
        //Arrange
        const int PlayerId = 3;
        _currentUserId = 9;
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);

        //Act
        var action = () => _sut.AddPlayer(_gameRoom.Id, PlayerId);

        //Assert
        await action.Should().ThrowAsync<ActionNotAllowedException>()
            .WithMessage($"User has not rights to Update game room (ID {_gameRoom.Id})");
    }

    [Fact]
    public async Task RemovePlayer_ShouldTriggerRemovePlayer_ShouldPass()
    {
        //Arrange
        const int PlayerId = 3;

        //Act
        await _sut.RemovePlayer(_gameRoom.Id, PlayerId);

        //Assert
        _gameRoomRepoMock.Verify(x => x.RemovePlayerById(_gameRoom.Id, PlayerId), Times.Once);
    }
    
    [Fact]
    public async Task RemovePlayer_ShouldThrowException_WhenUserNotMaster()
    {
        //Arrange
        const int PlayerId = 3;
        _currentUserId = 9;
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);

        //Act
        var action = () => _sut.RemovePlayer(_gameRoom.Id, PlayerId);

        //Assert
        await action.Should().ThrowAsync<ActionNotAllowedException>()
            .WithMessage($"User has not rights to Update game room (ID {_gameRoom.Id})");
    }
}