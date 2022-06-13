using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;
public class Order : IEntity
{
    public int Id { get; set; }
    public OrderState State { get; set; } = OrderState.Placed;
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public OrderReceiptMethod OrderReceiptMethod { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime PlacedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
    
    public User User { get; set; }
    public int UserId { get; set; }

    public IEnumerable<OrderProduct> Products { get; set; } = new List<OrderProduct>();
}
