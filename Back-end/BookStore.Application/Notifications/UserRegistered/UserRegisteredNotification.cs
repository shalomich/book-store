using MediatR;

namespace BookStore.Application.Notifications.UserRegistered;
public record UserRegisteredNotification(int UserId) : INotification;


