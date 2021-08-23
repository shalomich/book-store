using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class BasketProduct : IEntity
    {
        public int Id { set; get; }
        public virtual Basket Basket { set; get; }
        public int BasketId { set; get; }
        public virtual Product Product { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; } = 1;

        public override bool Equals(object obj)
        {
            return obj is BasketProduct basketProduct
                && BasketId == basketProduct.BasketId
                && ProductId == basketProduct.ProductId;
                   
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BasketId, ProductId);
        }
    }
}
