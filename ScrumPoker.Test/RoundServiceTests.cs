using System.Threading.Tasks;
using FluentAssertions;
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
    private readonly Mock<IRoundStateService> _roundStateServiceMock = new();

    private readonly Round _round;
    private readonly Round _newRound;
    private readonly GameRoom _gameRoom;
    private int _currentUserId = 2;


    public RoundServiceTests()
    {
        _sut = new RoundService(_roundRepoMock.Object, _gameRoomServiceMock.Object, _userManagerMock.Object,
            _roundStateServiceMock.Object);

        _round = new Round
        {
            Description = "Description",
            RoundId = 1,
            RoundState = RoundState.Grooming,
            GameRoomId = 1
        };

        _newRound = new Round
        {
            Description = "Description",
            RoundId = 2,
            RoundState = RoundState.Grooming,
            GameRoomId = 1
        };

        _gameRoom = new GameRoom
        {
            Id = 1,
            MasterId = 2,
            CurrentRoundId = _currentUserId
        };

        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _roundRepoMock.Setup(x => x.GetById(_round.RoundId))
            .ReturnsAsync(_round);
        _gameRoomServiceMock.Setup(x => x.GetById(_gameRoom.Id))!
            .ReturnsAsync(_gameRoom);
    }

    [Fact]
    public async Task GetById_ShouldReturnRound_WhenRoundExist()
    {
        //Act
        var round = await _sut.GetById(1);

        //Assert
        round.Should().Be(_round);
        round.Description.Should().Be(_round.Description);
    }

    [Fact]
    public async Task GetById_ShouldReturnNothing_WhenRoundDoesNotExist()
    {
        //Arrange
        const int getId = 2;
        _roundRepoMock.Setup(x =>
                x.GetById(getId))
            .ReturnsAsync(() => null!);

        //Act
        var round = await _sut.GetById(getId);

        //Assert
        round.Should().BeNull();
    }

    [Fact]
    public async Task CreateRound_ShouldCreateNewRound_ShouldReturnNewRound()
    {
        //Arrange
        _roundRepoMock.Setup(x => x.Create(_round))
            .ReturnsAsync(_newRound);


        //Act
        var round = await _sut.Create(_round);

        //Assert
        round.Should().Be(_newRound);
    }


    [Fact]
    public async Task CreateRound_ShouldThrowException_WhenUserNotMaster()
    {
        //Arrange
        _currentUserId = 9;
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _roundRepoMock.Setup(x => x.Create(_round))
            .ReturnsAsync(_newRound);

        //Act
        var action = async () => await _sut.Create(_round);

        await action.Should().ThrowAsync<ActionNotAllowedException>()
            .WithMessage($"User has not rights to Update game room (ID {_gameRoom.Id})");
    }

    [Fact]
    public async Task SetState_ShouldTriggerRoundRepositorySetState_ShouldPass()
    {
        //Arrange
        var roundChangeStateRequest = new Round {RoundId = 1, RoundState = RoundState.VoteRegistration};
        _roundRepoMock.Setup(x => x.GetById(roundChangeStateRequest.RoundId))
            .ReturnsAsync(_round);

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
        _roundRepoMock.Setup(x => x.GetById(roundChangeStateRequest.RoundId))
            .ReturnsAsync(_round);
        _roundStateServiceMock.Setup(x => x.ValidateRoundState(roundChangeStateRequest, _round))
            .Throws(new InvalidRoundStateException(
                $"Round state {roundChangeStateRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}"));

        //Assert
        var action = () => _sut.SetState(roundChangeStateRequest);

        await action.Should().ThrowAsync<InvalidRoundStateException>()
            .WithMessage(
                $"Round state {roundChangeStateRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }

    [Fact]
    public async Task SetState_ShouldFailWhenStateIsFinished_ShouldThrowInvalidRoundStateException()
    {
        //Arrange
        _round.RoundState = RoundState.Finished;
        var roundChangeStateRequest = new Round {RoundId = 1, RoundState = RoundState.VoteRegistration};
        _roundRepoMock.Setup(x => x.GetById(roundChangeStateRequest.RoundId))
            .ReturnsAsync(_round);

        //Assert
        var action = () => _sut.SetState(roundChangeStateRequest);

        await action.Should().ThrowAsync<InvalidRoundStateException>()
            .WithMessage("Round is finished, state cannot be changed!");
    }

    [Fact]
    public async Task SetState_ShouldFailWhenStateCurrentUserIsNotMaster_ShouldThrowActionNotAllowedException()
    {
        //Arrange
        _currentUserId = 9;
        var roundChangeStateRequest = new Round {RoundId = 1, RoundState = RoundState.VoteRegistration};
        _roundRepoMock.Setup(x => x.GetById(roundChangeStateRequest.RoundId))
            .ReturnsAsync(_round);
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);

        //Assert
        var action = () => _sut.SetState(roundChangeStateRequest);

        await action.Should().ThrowAsync<ActionNotAllowedException>()
            .WithMessage($"User has not rights to Update game room (ID {_gameRoom.Id})");
    }

    [Fact]
    public async Task Update_ShouldUpdateRoundDescription_ShouldPass()
    {
        //Arrange
        var roundUpdateRequest = new Round {RoundId = 1, Description = "new Description"};
        _roundRepoMock.Setup(x => x.GetById(roundUpdateRequest.RoundId))!
            .ReturnsAsync(_round);

        //Act
        await _sut.Update(roundUpdateRequest);

        //Assert
        _roundRepoMock.Verify(x => x.Update(roundUpdateRequest), Times.Once);
    }

    [Fact]
    public async Task Update_ShouldThrowException_WhenUserNotMaster()
    {
        //Arrange
        var roundUpdateRequest = new Round {RoundId = 1, Description = "new Description"};
        _currentUserId = 9;
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _roundRepoMock.Setup(x => x.GetById(roundUpdateRequest.RoundId))!
            .ReturnsAsync(_round);

        //Act
        var action = () => _sut.Update(roundUpdateRequest);

        await action.Should().ThrowAsync<ActionNotAllowedException>()
            .WithMessage($"User has not rights to Update game room (ID {_gameRoom.Id})");
    }
}