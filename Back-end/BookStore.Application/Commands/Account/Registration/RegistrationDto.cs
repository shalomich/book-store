using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Account.Registration;

public record RegistrationDto
{
    [Required]
    [Email]
    public string Email { init; get; }

    [Required]
    public string FirstName { init; get; }

    [Required]
    public string Password { init; get; }
}

