using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScrumPoker.Business.Interfaces.Interfaces;

namespace ScrumPoker.Business;

public abstract class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    protected AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(int id)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_configuration["JWT:PrivateKey"]));
        var credentials = new SigningCredentials(key: key, algorithm: SecurityAlgorithms.HmacSha256);

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
}