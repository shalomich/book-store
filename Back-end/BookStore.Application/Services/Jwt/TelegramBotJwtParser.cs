using BookStore.Application.Providers;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace BookStore.Application.Services.Jwt;
public class TelegramBotJwtParser
{
    private JwtSettings JwtSettings { get; }
    public TelegramBotJwtParser(IConfiguration configuration)
    {
        JwtSettings = configuration.GetSection("Auth:Jwt:TelegramBot").Get<JwtSettings>();
    }

    public ClaimsPrincipal FromToken(string token, bool checkExpiration = true)
    {
        return JwtParser.FromToken(token, JwtSettings, checkExpiration);
    }

    public string ToToken(ClaimsPrincipal principal)
    {
        return JwtParser.ToToken(principal, JwtSettings);
    }
}
