using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities.Products
{
    public abstract class Product : IFormEntity
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public const int MinCost = 100;
        public const int MinQuantity = 0;
        public const int MaxDescriptionLength = 1000;

        private int _cost;
        private int _quantity;
        private string _description;

        public int Cost
        {
            set
            {
                if (value < MinCost)
                    throw new ArgumentOutOfRangeException();
                _cost = value;
            }
            get
            {
                return _cost;
            }
        }

        public int Quantity 
        {
            set
            {
                if (value < MinQuantity)
                    throw new ArgumentOutOfRangeException();
                _quantity = value;
            }
            get
            {
                return _quantity;
            }
        }

        public string Description
        {
            set
            {
                if (value?.Length > MaxDescriptionLength)
                    throw new ArgumentException();
                _description = value;
            }
            get
            {
                return _description;
            }
        }

        public DateTime AddingDate {private set; get; } = DateTime.Today;

        public Album Album { set; get; }

        public ISet<BasketProduct> BasketProducts { set; get; }
    }
}
