using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Account.Login;

public record LoginDto
{
    [Required]
    [Email]
    public string Email { init; get; }


    [Required]
    public string Password { init; get; }
}

