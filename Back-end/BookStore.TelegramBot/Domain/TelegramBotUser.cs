using BookStore.Application.Commands.Account.Common;
using BookStore.TelegramBot.Providers;
using System.ComponentModel.DataAnnotations;

namespace BookStore.TelegramBot.Domain;
internal class TelegramBotUser
{
    public int Id { get; set; }
    public long TelegramId { get; set; }
    
    [Required]
    public string AccessToken { get; set; }
    
    [Required]
    public string RefreshToken { get; set; }

    public DateTimeOffset AccessTokenExpiration { get; set; }
    public DateTimeOffset RefreshTokenExpiration { get; set; }
    public CallbackCommand CallbackCommand { get; set; }
    public int? CallbackCommandId { get; set; }

    public void SetUserInfo(TokensDto tokens, BackEndSettings settings)
    {
        AccessToken = tokens.AccessToken;
        RefreshToken = tokens.RefreshToken;
        AccessTokenExpiration = DateTimeOffset.UtcNow
                .AddMinutes(settings.AccessTokenExpiredMinutes);
        RefreshTokenExpiration = DateTimeOffset.UtcNow
                .AddMinutes(settings.RefreshTokenExpiredMinutes);
    }
}

