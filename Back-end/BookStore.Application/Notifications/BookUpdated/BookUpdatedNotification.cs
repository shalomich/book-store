using MediatR;

namespace BookStore.Application.Notifications.BookUpdated;

public record BookUpdatedNotification(int BookId) : INotification;

