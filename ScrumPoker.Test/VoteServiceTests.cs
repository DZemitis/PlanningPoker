using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ScrumPoker.Business;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.ForbiddenExceptions;
using ScrumPoker.Common.Models;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Interfaces;
using Xunit;

namespace ScrumPoker.Test;

public class VoteServiceTests
{
    private readonly VoteService _sut;
    private readonly Mock<IGameRoomService> _gameRoomServiceMock = new();
    private readonly Mock<IRoundService> _roundServiceMock = new();
    private readonly Mock<IUserManager> _userManagerMock = new();
    private readonly Mock<IVoteRepository> _voteRepoMock = new();

    private readonly Vote _vote;
    private int _currentUserId = 2;
    private readonly Round _round;
    private readonly GameRoom _gameRoom;
    private readonly Player _player;

    public VoteServiceTests()
    {
        _sut = new VoteService(_voteRepoMock.Object, _userManagerMock.Object, _roundServiceMock.Object,
            _gameRoomServiceMock.Object);

        _player = new Player
        {
            Id = 2
        };

        _round = new Round
        {
            RoundId = 3,
            RoundState = RoundState.VoteRegistration,
            GameRoomId = 1
        };

        _vote = new Vote
        {
            Id = 1,
            PlayerId = _player.Id,
            RoundId = _round.RoundId,
            VoteResult = 4
        };


        _gameRoom = new GameRoom
        {
            Id = 1,
            Players = new List<Player>
            {
                _player
            },
            Round = _round,
            MasterId = 2
        };

        _voteRepoMock.Setup(x => x.GetById(_vote.Id))
            .ReturnsAsync(_vote);
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        _roundServiceMock.Setup(x => x.GetById(_vote.RoundId))
            .ReturnsAsync(_round);
        _gameRoomServiceMock.Setup(x => x.GetById(_round.GameRoomId))
            .ReturnsAsync(_gameRoom);
    }

    [Fact]
    public async Task GetById_ShouldReturnVote_WhenExist()
    {
        //Act
        var vote = await _sut.GetById(1);

        //Arrange
        vote.Should().Be(_vote);
    }

    [Fact]
    public async Task GetById_ShouldReturnNothing_WhenDoesNotExist()
    {
        //Arrange
        _voteRepoMock.Setup(x => x.GetById(2))
            .ReturnsAsync(() => null!);

        //Act
        var vote = await _sut.GetById(2);

        //Arrange
        vote.Should().BeNull();
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldCreateVote_ShouldReturnVote()
    {
        //Arrange
        var createVoteRequest = new Vote {RoundId = 3, VoteResult = 4};
        _voteRepoMock.Setup(x => x.CreateOrUpdate(createVoteRequest))
            .ReturnsAsync(_vote);

        //Act
        var vote = await _sut.CreateOrUpdate(createVoteRequest);

        //Assert
        vote.Should().Be(_vote);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldThrowExceptionWhenRoundStateIsFinished_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.Finished;
        var createVoteRequest = new Vote {RoundId = 3, VoteResult = 4};
        _roundServiceMock.Setup(x => x.GetById(_round.RoundId))
            .ReturnsAsync(_round);

        //Act
        var action = () => _sut.CreateOrUpdate(createVoteRequest);

        //Assert
        await action.Should().ThrowAsync<InvalidRoundStateException>()
            .WithMessage("You are allowed to vote only when round state is - vote registration");
    }


    [Fact]
    public async Task CreateOrUpdate_ShouldThrowExceptionWhenPlayerIsNotInRoom_ShouldThrowException()
    {
        //Arrange
        _currentUserId = 9;
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);
        var createVoteRequest = new Vote {RoundId = 3, VoteResult = 4};

        //Act
        var action = () => _sut.CreateOrUpdate(createVoteRequest);

        //Assert
        await action.Should().ThrowAsync<IdNotFoundException>()
            .WithMessage($"No user with ID {_currentUserId} found in game room ID {_gameRoom.Id}");
    }

    [Fact]
    public async Task ClearRoundVotes_ShouldTriggerClearVotes_ShouldPass()
    {
        //Act
        await _sut.ClearRoundVotes(_round.RoundId);

        //Assert
        _voteRepoMock.Verify(x => x.ClearRoundVotes(_round.RoundId), Times.Once);
    }

    [Fact]
    public async Task ClearRoundVotes_ShouldThrownExceptionWhenUserIsNotMaster_ShouldThrowException()
    {
        //Arrange
        _currentUserId = 9;
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(_currentUserId);

        //Act
        var action = () => _sut.ClearRoundVotes(_round.RoundId);

        //Assert
        await action.Should().ThrowAsync<ActionNotAllowedException>()
            .WithMessage($"User has not rights to Update game room (ID {_gameRoom.Id})");
    }

    [Fact]
    public async Task ClearRoundVotes_ShouldThrownExceptionWhenRoundIsFinished_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.Finished;
        _roundServiceMock.Setup(x => x.GetById(_round.RoundId))
            .ReturnsAsync(_round);

        //Act
        var action = () => _sut.ClearRoundVotes(_round.RoundId);

        //Assert
        await action.Should().ThrowAsync<InvalidRoundStateException>()
            .WithMessage("Round is finished, any updates on votes is unavailable!");
    }
}