using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record OrderMakingDto
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; set; }
        public OrderReceiptMethod OrderReceiptMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
