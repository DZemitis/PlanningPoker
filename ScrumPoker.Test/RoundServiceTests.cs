using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Moq;
using ScrumPoker.Business;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Business.Models.Models;
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
            .ReturnsAsync(()=>null!);

        //Act
        var round = await _sut.GetById(getId);

        //Assert
        Assert.Null(round);
    }

    [Fact]
    public async Task CreateRound_ShouldCreateNewRound_ShouldPass()
    {
        //Arrange
        _gameRoom.Round = _newRound;
        _gameRoom.Rounds.Add(_newRound);
        _gameRoomServiceMock.Setup(x => x.GetById(_gameRoom.Id))
            .ReturnsAsync(_gameRoom);
        _roundRepoMock.Setup(x => x.Create(_round))
            .ReturnsAsync(_newRound);
        _userManagerMock.Setup(x => x.GetCurrentUserId())
            .Returns(2);
        
        //Act
        var round = await _sut.Create(_round);

        //Assert
        Assert.Equal(round, _newRound);
    }
}