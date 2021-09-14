using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Basket : IEntity
    {
        public int Id { set; get; }
        public virtual User User { set; get; }
        public int UserId { set; get; }
        public ISet<BasketProduct> BasketProducts { set; get; }

        public int? TotalAmount => BasketProducts?.Sum(basketProduct => basketProduct.Quantity
            * basketProduct.Product.Cost) ?? 0;

        public int? TotalQuantity => BasketProducts?.Sum(basketProduct => basketProduct.Quantity)?? 0;
    }
}
