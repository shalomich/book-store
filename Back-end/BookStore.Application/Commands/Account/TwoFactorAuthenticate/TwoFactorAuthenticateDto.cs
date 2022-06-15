using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Account.TwoFactorAuthenticate;
public record TwoFactorAuthenticateDto
{
    [Required]
    public string Code { get; set; }
}
