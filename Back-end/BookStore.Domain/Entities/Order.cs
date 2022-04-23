using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public OrderState State { get; set; } = OrderState.Placed;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public OrderReceiptMethod OrderReceiptMethod { get; set; }
        public PaymentMethod PaymentMethod  { get; set; }
        public ISet<OrderProduct> Products { get; set; }

    }
}
