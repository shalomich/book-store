using BookStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Orders.PlaceOrder;
public record OrderForm
{
    [Required]
    public string FirstName { get; set; }
        
    [Required]
    public string LastName { get; set; }
        
    [Required]
    [EmailAddress]
    public string Email { get; set; }
        
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    [Required]
    public OrderReceiptMethod? OrderReceiptMethod { get; set; }

    [Required]
    public PaymentMethod? PaymentMethod { get; set; }
}

