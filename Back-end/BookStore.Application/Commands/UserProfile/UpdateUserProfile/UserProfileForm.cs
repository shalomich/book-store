﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.UserProfile.UpdateUserProfile;

public record UserProfileForm
{
    [Required]
    public string FirstName { init; get; }
    public string LastName { init; get; }
    public string PhoneNumber { init; get; }
    public string Address { init; get; }
}

