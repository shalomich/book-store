using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.ViewModels.Account;

public record RegistrationForm
{
    [Required]
    [Email]
    public string Email { init; get; }

    [Required]
    public string UserName { init; get; }

    [Required]
    public string Password { init; get; }
}

