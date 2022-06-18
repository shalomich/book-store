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
}

