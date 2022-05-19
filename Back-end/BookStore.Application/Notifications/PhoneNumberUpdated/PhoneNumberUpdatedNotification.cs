using MediatR;

namespace BookStore.Application.Notifications.PhoneNumberUpdated;
public record PhoneNumberUpdatedNotification(int UserId) : INotification;
