using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ScrumPoker.Business;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using Xunit;

namespace ScrumPoker.Test;

public class PlayerServiceTests
{
    private readonly PlayerService _sut;
    private readonly Mock<IUserManager> _userManagerMock = new();
    private readonly Mock<IPlayerRepository> _playerRepoMock = new();

    private const int CurrentUserId = 2;
    private readonly Player _player;
    private readonly List<Player> _playerList;

    public PlayerServiceTests()
    {
        _sut = new PlayerService(_playerRepoMock.Object, _userManagerMock.Object);

        _player = new Player
        {
            Email = "davis@gmail.com",
            Id = 2,
            Name = "Davis",
        };

        _playerList = new List<Player>
        {
            _player
        };

        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(CurrentUserId);
        _playerRepoMock.Setup(x => x.GetById(_player.Id))
            .ReturnsAsync(_player);
    }

    [Fact]
    public async Task GetAll_ShouldReturnListOfPlayers_ShouldPass()
    {
        //Arrange
        _playerRepoMock.Setup(x => x.GetAll()).ReturnsAsync(_playerList);

        //Act
        var gameRoomList = await _sut.GetAll();

        //Assert
        gameRoomList.Should().Contain(_player);
    }

    [Fact]
    public async Task GetById_ShouldReturnPlayer_WhenExist()
    {
        //Act
        var player = await _sut.GetById(2);

        //Assert
        player.Should().Be(_player);
    }

    [Fact]
    public async Task GetById_ShouldReturnNothing_WhenDoesNotExist()
    {
        //Arrange
        var getId = 9;
        _playerRepoMock.Setup(x => x.GetById(getId))
            .ReturnsAsync(() => null!);

        //Act
        var player = await _sut.GetById(getId);

        //Assert
        player.Should().BeNull();
    }

    [Fact]
    public async Task Create_ShouldCreatePlayer_ShouldPass()
    {
        //Arrange
        var playerCreateRequest = new Player {Name = "Davis", Email = "davis@gmail.com"};
        _playerRepoMock.Setup(x => x.Create(playerCreateRequest))
            .ReturnsAsync(_player);

        //Act
        var player = await _sut.Create(playerCreateRequest);

        //Assert
        player.Should().Be(_player);
    }

    [Fact]
    public async Task Update_ShouldTriggerUpdatePlayer_ShouldPass()
    {
        //Arrange
        var playerUpdateRequest = new Player {Name = "Janis", Email = "davis@gmail.com"};

        //Act
        await _sut.Update(playerUpdateRequest);

        //Arrange
        _playerRepoMock.Verify(x => x.Update(playerUpdateRequest), Times.Once);
    }

    [Fact]
    public async Task DeleteById_ShouldTriggerDeleteById_ShouldPass()
    {
        //Arrange
        _playerRepoMock.Setup(x => x.DeleteById(_player.Id));

        //Act
        await _sut.DeleteById(_player.Id);

        //Arrange
        _playerRepoMock.Verify(x => x.DeleteById(_player.Id), Times.Once);
    }
}