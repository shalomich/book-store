using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class OrderProduct : IEntity
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Cost { get; set; }

        public override bool Equals(object obj)
        {
            return obj is OrderProduct product &&
                   OrderId == product.OrderId &&
                   ProductId == product.ProductId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, ProductId);
        }
    }
}
