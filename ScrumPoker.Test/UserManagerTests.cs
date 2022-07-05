using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        
        var _userClaims = new List<Claim>
        {
            new("userId", "2")
        };
        
        _httpContextMock.Setup(x => x.HttpContext!.User.Claims)
            .Returns(_userClaims);
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