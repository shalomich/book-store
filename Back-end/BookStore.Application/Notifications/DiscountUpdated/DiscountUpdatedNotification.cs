using MediatR;

namespace BookStore.Application.Notifications.DiscountUpdated;

public record DiscountUpdatedNotification(int ProductId) : INotification;