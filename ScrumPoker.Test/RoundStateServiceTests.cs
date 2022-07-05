using System;
using FluentAssertions;
using ScrumPoker.Business;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.ConflictExceptions;
using ScrumPoker.Common.Models;
using Xunit;

namespace ScrumPoker.Test;

public class RoundStateServiceTests
{
    private readonly RoundStateService _sut;

    private readonly Round _roundRequest;
    private readonly Round _round;

    public RoundStateServiceTests()
    {
        _sut = new RoundStateService();

        _round = new Round
        {
            RoundState = RoundState.Grooming
        };

        _roundRequest = new Round
        {
            RoundState = RoundState.VoteRegistration
        };
    }

    [Fact]
    public void ValidateRoundState_WhenValidRoundStateRequest_ShouldPass()
    {
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);

        //Arrange
        action.Should().NotThrow();
    }

    [Fact]
    public void ValidateRoundState_GroomingToVoteReview_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.Grooming;
        _roundRequest.RoundState = RoundState.VoteReview;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }

    [Fact]
    public void ValidateRoundState_GroomingToFinished_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.Grooming;
        _roundRequest.RoundState = RoundState.Finished;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }
    
    
    [Fact]
    public void ValidateRoundState_VoteRegToVoteReview_ShouldPass()
    {
        //Arrange
        _round.RoundState = RoundState.VoteRegistration;
        _roundRequest.RoundState = RoundState.VoteReview;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().NotThrow();
    }

    [Fact]
    public void ValidateRoundState_VoteRegToGrooming_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.VoteRegistration;
        _roundRequest.RoundState = RoundState.Grooming;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_VoteRegToFinished_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.VoteRegistration;
        _roundRequest.RoundState = RoundState.Finished;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_VoteReviewToGrooming_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.VoteReview;
        _roundRequest.RoundState = RoundState.Grooming;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_VoteReviewToGrooming_ShouldPass()
    {
        //Arrange
        _round.RoundState = RoundState.VoteReview;
        _roundRequest.RoundState = RoundState.VoteRegistration;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().NotThrow();
    }
    
    [Fact]
    public void ValidateRoundState_VoteReviewToFinished_ShouldPass()
    {
        //Arrange
        _round.RoundState = RoundState.VoteReview;
        _roundRequest.RoundState = RoundState.VoteRegistration;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().NotThrow();
    }
    
    
    [Fact]
    public void ValidateRoundState_FinishedToGrooming_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.Finished;
        _roundRequest.RoundState = RoundState.Grooming;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_FinishedToVoteReg_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.Finished;
        _roundRequest.RoundState = RoundState.VoteRegistration;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_FinishedToVoteReview_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = RoundState.Finished;
        _roundRequest.RoundState = RoundState.VoteReview;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundRequest.RoundState.ToString()} is not allowed after {_round.RoundState.ToString()}");
    }
    
    
    [Fact]
    public void ValidateRoundState_OutOfRange_ShouldThrowException()
    {
        //Arrange
        _round.RoundState = (RoundState)99;
        _roundRequest.RoundState = RoundState.VoteReview;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundRequest, _round);
        
        //Arrange
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Specified argument was out of the range of valid values.");
    }
}