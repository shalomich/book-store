using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Subscriptions.UpdateSubscription;

public record UpdateSubscriptionDto
{
    [Required]
    public bool? MarkNotificationEnable { get; init; }
    
    [Required]
    public bool? TagNotificationEnable { get; init; }
}

