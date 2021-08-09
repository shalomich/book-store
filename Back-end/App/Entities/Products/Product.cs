using App.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Various;

namespace App.Entities
{
    public abstract class Product : Entity
    {      
        private int _cost;
       
        public int Cost
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();
                _cost = value;
            }
            get
            {
                return _cost;
            }
        }

        public uint Quantity { set; get; }

        public string Description { set; get; }

        public DateTime AddingDate {private set; get; } = DateTime.Today;

        public Album Album { set; get; }


    }
}
