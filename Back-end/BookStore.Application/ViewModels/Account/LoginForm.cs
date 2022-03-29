using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.ViewModels.Account;

public record LoginForm
{
    [Required]
    [Email]
    public string Email { init; get; }


    [Required]
    public string Password { init; get; }
}

