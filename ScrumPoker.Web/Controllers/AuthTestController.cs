using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ScrumPoker.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthTestController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthTestController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    [Route("CreateToken/{id:int}")]
    public IActionResult CreateToken(int id)
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
        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }

    [HttpGet]
    [Authorize]
    [Route("CheckToken")]
    public IActionResult CheckAuth()
    {
        var claim = HttpContext.User.Claims.Single(x => x.Type == "userId");
        return Ok(claim.Value);
    }
}