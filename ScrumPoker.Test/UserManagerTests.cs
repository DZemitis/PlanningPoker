using System;
using System.IdentityModel.Tokens.Jwt;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using ScrumPoker.Business;
using Xunit;

namespace ScrumPoker.Test;

public class UserManagerTests
{
    private readonly UserManager _sut;
    private readonly Mock<IHttpContextAccessor> _httpContextMock = new();
    private readonly Mock<IConfiguration> _configurationMock = new();
      

    public UserManagerTests()
    {
        _sut = new UserManager(_httpContextMock.Object, _configurationMock.Object);
        
        _configurationMock.Setup(x=>x.)
    }

    [Fact]
    public void CreateToken()
    {
        //Arrange
        var stream = $"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIyIiwibmJmIjoxNjU2OTMxMTQ4LCJleHAiOjE2NTY5MzgzNDgsImlzcyI6IlNjcnVtUG9rZXIiLCJhdWQiOiJTY3J1bVBva2VyIn0.MkLZV1FPlN7mX5iPaxg17WBDXw8Pe7bwRQQOv55Z0zw";  
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(stream);
        Console.WriteLine(jsonToken);
        
        //Act
       
        

        //Assert
        x.Should().Be("2");

    }
}