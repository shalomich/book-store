using BookStore.Domain.Enums;
using System.Collections.Generic;
using System;

namespace BookStore.Application.Queries.Order.GetOrders;

public record OrderDto
{
    public int Id { get; set; }
    public string State { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string OrderReceiptMethod { get; set; }
    public string PaymentMethod { get; set; }
    public string PlacedDate { get; set; }
    public string DeliveredDate { get; set; }
    public IEnumerable<OrderProductDto> Products { get; set; }
}

