using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class BasketProduct : IEntity
    {
        private const int MinQuantity = 1;
        private readonly static string MinQuantityMessage = $"Quantity can't be less than {MinQuantity}";

        private int _quantity;
        public int Id { set; get; }
        public User User { set; get; }
        public int UserId { set; get; }
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

        public BasketProduct()
        {
            Quantity = MinQuantity;
        }

        public override bool Equals(object obj)
        {
            return obj is BasketProduct basketProduct
                && UserId == basketProduct.UserId
                && ProductId == basketProduct.ProductId;
                   
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, ProductId);
        }
    }
}
