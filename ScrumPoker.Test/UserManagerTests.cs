using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using ScrumPoker.Business;
using Xunit;

namespace ScrumPoker.Test;

public class UserManagerTests
{
    private readonly UserManager _sut;
    private readonly Mock<IHttpContextAccessor> _httpContextMock = new();

    private readonly int _currentUserId = 2;


    public UserManagerTests()
    {
        _sut = new UserManager(_httpContextMock.Object);

        _httpContextMock.Setup(x => x.HttpContext!.User.Claims.Single(x => x.Type == "userId").Value)
            .Returns($"{_currentUserId}");
    }

    [Fact]
    public void CreateToken()
    {
        //Act
        var result = _sut.GetCurrentUserId();

        //Assert
        result.Should().Be(_currentUserId);
    }
}