using App.Entities.Products;
using System;
using System.Collections.Generic;

namespace App.Entities
{
    public abstract class Product : IFormEntity
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public const int MinCost = 100;
        public const int MaxDescriptionLength = 1000;

        private int _cost;
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

        public uint Quantity { set; get; }

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
