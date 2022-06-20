using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Account.Common;
public record TokensDto
{
    [Required]
    public string AccessToken { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}

