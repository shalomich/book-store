using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Products
{
    public class ProductTag : IEntity
    {
        public int Id { set; get; }

        public Product Product { set; get; }
        public int ProductId { set; get; }

        public Tag Tag { set; get; }
        public int TagId { set; get; }

        public override bool Equals(object obj)
        {
            return obj is ProductTag bookTag &&
                   ProductId == bookTag.ProductId &&
                   TagId == bookTag.TagId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, TagId);
        }
    }
}
