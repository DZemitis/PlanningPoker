using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ScrumPoker.Business;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.ForbiddenExceptions;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Interfaces;
using Xunit;

namespace ScrumPoker.Test;

public class RoundServiceTests
{
    private readonly RoundService _sut;
    private readonly Mock<IGameRoomService> _gameRoomServiceMock = new();
    private readonly Mock<IRoundRepository> _roundRepoMock = new();
    private readonly Mock<IUserManager> _userManagerMock = new();

    private readonly Round _round;
    private readonly Round _newRound;
    private readonly GameRoom _gameRoom;
    private int _currentUserId = 2;


    public RoundServiceTests()
    {
        _sut = new RoundService(_roundRepoMock.Object, _gameRoomServiceMock.Object, _userManagerMock.Object);
        _round = new Round
        {
            Description = "Description",
            RoundId = 1,
            RoundState = RoundState.Grooming,
            GameRoomId = 1,
            Votes = new List<Vote>
            {
                new()
                {
                    Id = 1,
                    PlayerId = 9,
                    RoundId = 1,
                    VoteResult = 2
                }
            }
        };

        _newRound = new Round
        {
            Description = "Description",
            RoundId = 2,
            RoundState = RoundState.Grooming,
            GameRoomId = 1,
            Votes = new List<Vote>()
        };

        _gameRoom = new GameRoom
        {
            Id = 1,
            Name = "Game Room",
            Players = new List<Player>(),
            Round = _round,
            Rounds = new List<Round>
            {
                _round
            },
            MasterId = 2,
            CurrentRoundId = 2
        };
    }

    [Fact]
    public async Task GetById_ShouldReturnRound_WhenRoundExist()
    {
        //Arrange
        _roundRepoMock.Setup(x =>
                x.GetById(_round.RoundId))
            .ReturnsAsync(_round);

        //Act
        var round = await _sut.GetById(1);

        //Assert
        Assert.Equal(round, _round);
        Assert.Equal(round.Description, _round.Description);
    }

    [Fact]
    public async Task GetById_ShouldReturnNothing_WhenRoundDoesNotExist()
    {
        //Arrange
        var getId = 2;
        _roundRepoMock.Setup(x =>
                x.GetById(getId))
            .ReturnsAsync(() => null!);

        //Act
        var round = await _sut.GetById(getId);

        //Assert
        Assert.Null(round);
    }

    [Fact]
    public async Task CreateRound_ShouldCreateNewRound_ShouldReturnNewRound()
    {
        //Arrange
        _gameRoomServiceMock.Setup(x => x.GetById(_gameRoom.Id))
            .ReturnsAsync(_gameRoom);
        _roundRepoMock.Setup(x => x.Create(_round))
            .ReturnsAsync(_newRound);
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);

        //Act
        var round = await _sut.Create(_round);

        //Assert
        Assert.Equal(round, _newRound);
    }

    [Fact]
    public async Task SetState_ShouldTriggerRoundRepositorySetState_ShouldPass()
    {
        //Arrange
        var roundChangeStateRequest = new Round {RoundId = 1, RoundState = RoundState.VoteRegistration};
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _roundRepoMock.Setup(x => x.GetById(roundChangeStateRequest.RoundId))
            .ReturnsAsync(_round);
        _gameRoomServiceMock.Setup(x => x.GetById(_gameRoom.Id))
            .ReturnsAsync(_gameRoom);

        //Act
        await _sut.SetState(roundChangeStateRequest);

        //Assert
        _roundRepoMock.Verify(x => x.SetState(roundChangeStateRequest), Times.Once);
    }

    [Fact]
    public async Task SetState_ShouldFailWhenItsNotAllowedState_ShouldThrowInvalidRoundStateException()
    {
        //Arrange
        var roundChangeStateRequest = new Round {RoundId = 1, RoundState = RoundState.Finished};
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _roundRepoMock.Setup(x => x.GetById(roundChangeStateRequest.RoundId))
            .ReturnsAsync(_round);
        _gameRoomServiceMock.Setup(x => x.GetById(_gameRoom.Id))
            .ReturnsAsync(_gameRoom);

        //Assert
        var ex = Assert.ThrowsAsync<InvalidRoundStateException>(() =>
            _sut.SetState(roundChangeStateRequest)).Result.Message;

        Assert.Equal(
            $"Round state {roundChangeStateRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}",
            ex);
        await Assert.ThrowsAsync<InvalidRoundStateException>(() => _sut.SetState(roundChangeStateRequest));
    }

    [Fact]
    public async Task SetState_ShouldFailWhenStateIsFinished_ShouldThrowInvalidRoundStateException()
    {
        //Arrange
        _round.RoundState = RoundState.Finished;
        var roundChangeStateRequest = new Round {RoundId = 1, RoundState = RoundState.VoteRegistration};
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _roundRepoMock.Setup(x => x.GetById(roundChangeStateRequest.RoundId))
            .ReturnsAsync(_round);
        _gameRoomServiceMock.Setup(x => x.GetById(_gameRoom.Id))
            .ReturnsAsync(_gameRoom);

        //Assert
        var ex = Assert.ThrowsAsync<InvalidRoundStateException>(() =>
            _sut.SetState(roundChangeStateRequest)).Result.Message;

        Assert.Equal("Round is finished, state cannot be changed!", ex);
        await Assert.ThrowsAsync<InvalidRoundStateException>(() => _sut.SetState(roundChangeStateRequest));
    }

    [Fact]
    public async Task SetState_ShouldFailWhenStateCurrentUserIsNotMaster_ShouldThrowActionNotAllowedException()
    {
        //Arrange
        _currentUserId = 9;
        var roundChangeStateRequest = new Round {RoundId = 1, RoundState = RoundState.VoteRegistration};
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _roundRepoMock.Setup(x => x.GetById(roundChangeStateRequest.RoundId))
            .ReturnsAsync(_round);
        _gameRoomServiceMock.Setup(x => x.GetById(_gameRoom.Id))
            .ReturnsAsync(_gameRoom);

        //Assert
        var ex = Assert.ThrowsAsync<ActionNotAllowedException>(() =>
            _sut.SetState(roundChangeStateRequest)).Result.Message;

        Assert.Equal($"User has not rights to Update game room (ID {_gameRoom.Id})", ex);
        await Assert.ThrowsAsync<ActionNotAllowedException>(() => _sut.SetState(roundChangeStateRequest));
    }


    [Fact]
    public async Task Update_ShouldUpdateRoundDescription_ShouldPass()
    {
        //Arrange
        var roundUpdateRequest = new Round {RoundId = 1, Description = "new Description"};
        _roundRepoMock.Setup(x => x.GetById(roundUpdateRequest.RoundId))
            .ReturnsAsync(_round);
        _gameRoomServiceMock.Setup(x => x.GetById(_round.GameRoomId))
            .ReturnsAsync(_gameRoom);
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);

        //Act
        await _sut.Update(roundUpdateRequest);

        //Assert
        _roundRepoMock.Verify(x => x.Update(roundUpdateRequest), Times.Once);
    }
}