
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Account.ResetPassword;
public record ResetPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Code { get; set; }
}
