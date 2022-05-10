using BookStore.Application.Providers;
using Microsoft.Extensions.Configuration;

namespace BookStore.Application.Services.Jwt;
public class TelegramBotJwtParser
{
    private JwtSettings JwtSettings { get; }
    public TelegramBotJwtParser(IConfiguration configuration)
    {
        JwtSettings = configuration.GetSection("Auth:Jwt:TelegramBot").Get<JwtSettings>();
    }

    public int FromToken(string token, bool checkExpiration = true)
    {
        return JwtParser.FromToken(token, JwtSettings, checkExpiration);
    }

    public string ToToken(int userId)
    {
        return JwtParser.ToToken(userId, JwtSettings);
    }
}
