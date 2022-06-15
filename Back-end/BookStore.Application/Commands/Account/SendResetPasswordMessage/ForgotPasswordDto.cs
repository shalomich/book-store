using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Account.SendResetPasswordMessage;
public record ForgotPasswordDto
{
    [Email]
    [Required]
    public string Email { get; init; }
}

