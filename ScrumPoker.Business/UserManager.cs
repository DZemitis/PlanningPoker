using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScrumPoker.Business.Interfaces.Interfaces;

namespace ScrumPoker.Business;

public class UserManager : IUserManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public UserManager(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public string CreateToken(int id)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_configuration["JWT:PrivateKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtHeader = new JwtHeader(credentials);
        var jwtClaims = new List<Claim>
        {
            new("userId", id.ToString())
        };

        var token = new JwtSecurityToken(
            jwtHeader,
            new JwtPayload(
                audience: "ScrumPoker",
                issuer: "ScrumPoker",
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(2),
                claims: jwtClaims
            )
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    
    public int GetCurrentUserId()
    {
        var currentUserId = _httpContextAccessor.HttpContext!.User.Claims.Single(x => x.Type == "userId").Value;

        return int.Parse(currentUserId);
    }
}