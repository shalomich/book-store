using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Products
{
    public class Mark : IEntity
    {
        public int Id { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Mark mark &&
                   UserId == mark.UserId &&
                   ProductId == mark.ProductId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, ProductId);
        }
    }
}
