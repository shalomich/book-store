using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.ViewModels.Profile;

public record UserProfileForm
{
    [Required]
    public string FirstName { init; get; }
    public string LastName { init; get; }
    public string PhoneNumber { init; get; }
    public string Address { init; get; }
}

