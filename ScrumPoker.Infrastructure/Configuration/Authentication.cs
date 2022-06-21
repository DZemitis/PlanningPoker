using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ScrumPoker.Infrastructure.Configuration;

public static class AuthenticationExtensions
{
    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF32.GetBytes(builder.Configuration["JWT:PrivateKey"])),
                        ValidAudience = "ScrumPoker",
                        ValidIssuer = "ScrumPoker",
                        RequireExpirationTime = true,
                        RequireAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = true
                    };
                }
            );
    }
}