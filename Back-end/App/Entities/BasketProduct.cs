using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class BasketProduct : IEntity
    {
        private const int MinQuantity = 1;
        private readonly static string MinQuantityMessage = $"Quantity can't be less than {MinQuantity}";

        private int _quantity;
        public int Id { set; get; }
        public Basket Basket { set; get; }
        public int BasketId { set; get; }
        public Product Product { set; get; }
        public int ProductId { set; get; }
        public int Quantity
        {
            set
            {
                if (value < MinQuantity)
                    throw new ArgumentException(MinQuantityMessage);
                _quantity = value;
            }
            get
            {
                return _quantity;
            }
        }

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
