using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ScrumPoker.Infrastructure.Auth;

public class JwtGenerator
{
    private readonly IConfiguration _configuration;

    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(int userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_configuration["JWT:PrivateKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtHeader = new JwtHeader(credentials);
        var jwtClaims = new List<Claim>
        {
            new("userId", userId.ToString())
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