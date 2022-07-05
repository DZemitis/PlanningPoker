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

    private RoundState _roundStateRequest;
    private RoundState _currentRoundState;

    public RoundStateServiceTests()
    {
        _sut = new RoundStateService();

        _currentRoundState = RoundState.Grooming;
        
        _roundStateRequest = RoundState.VoteRegistration;
    }

    [Fact]
    public void ValidateRoundState_WhenValidRoundStateRequest_ShouldPass()
    {
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);

        //Arrange
        action.Should().NotThrow();
    }

    [Fact]
    public void ValidateRoundState_GroomingToVoteReview_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = RoundState.Grooming;
        _roundStateRequest = RoundState.VoteReview;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundStateRequest.ToString()} is not allowed after {_currentRoundState.ToString()}");
    }

    [Fact]
    public void ValidateRoundState_GroomingToFinished_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = RoundState.Grooming;
        _roundStateRequest = RoundState.Finished;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundStateRequest.ToString()} is not allowed after {_currentRoundState.ToString()}");
    }
    
    
    [Fact]
    public void ValidateRoundState_VoteRegToVoteReview_ShouldPass()
    {
        //Arrange
        _currentRoundState = RoundState.VoteRegistration;
        _roundStateRequest = RoundState.VoteReview;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().NotThrow();
    }

    [Fact]
    public void ValidateRoundState_VoteRegToGrooming_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = RoundState.VoteRegistration;
        _roundStateRequest = RoundState.Grooming;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundStateRequest.ToString()} is not allowed after {_currentRoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_VoteRegToFinished_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = RoundState.VoteRegistration;
        _roundStateRequest = RoundState.Finished;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundStateRequest.ToString()} is not allowed after {_currentRoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_VoteReviewToGrooming_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = RoundState.VoteReview;
        _roundStateRequest = RoundState.Grooming;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundStateRequest.ToString()} is not allowed after {_currentRoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_VoteReviewToGrooming_ShouldPass()
    {
        //Arrange
        _currentRoundState = RoundState.VoteReview;
        _roundStateRequest = RoundState.VoteRegistration;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().NotThrow();
    }
    
    [Fact]
    public void ValidateRoundState_VoteReviewToFinished_ShouldPass()
    {
        //Arrange
        _currentRoundState = RoundState.VoteReview;
        _roundStateRequest = RoundState.VoteRegistration;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().NotThrow();
    }
    
    
    [Fact]
    public void ValidateRoundState_FinishedToGrooming_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = RoundState.Finished;
        _roundStateRequest = RoundState.Grooming;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundStateRequest.ToString()} is not allowed after {_currentRoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_FinishedToVoteReg_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = RoundState.Finished;
        _roundStateRequest = RoundState.VoteRegistration;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundStateRequest.ToString()} is not allowed after {_currentRoundState.ToString()}");
    }
    
    [Fact]
    public void ValidateRoundState_FinishedToVoteReview_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = RoundState.Finished;
        _roundStateRequest = RoundState.VoteReview;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<InvalidRoundStateException>()
            .WithMessage($"Round state {_roundStateRequest.ToString()} is not allowed after {_currentRoundState.ToString()}");
    }
    
    
    [Fact]
    public void ValidateRoundState_OutOfRange_ShouldThrowException()
    {
        //Arrange
        _currentRoundState = (RoundState)99;
        _roundStateRequest = RoundState.VoteReview;
        
        //Act
        var action = () => _sut.ValidateRoundState(_roundStateRequest, _currentRoundState);
        
        //Arrange
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Specified argument was out of the range of valid values.");
    }
}