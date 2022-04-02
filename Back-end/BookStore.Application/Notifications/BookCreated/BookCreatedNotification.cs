using MediatR;

namespace BookStore.Application.Notifications.BookCreated;

public record BookCreatedNotification(int BookId) : INotification;

