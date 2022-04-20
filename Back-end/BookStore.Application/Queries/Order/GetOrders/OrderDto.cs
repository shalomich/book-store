using BookStore.Domain.Enums;
using System.Collections.Generic;

namespace BookStore.Application.Queries.Order.GetOrders;

public record OrderDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public OrderReceiptMethod OrderReceiptMethod { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public IEnumerable<OrderProductDto> Products { get; set; }
}

