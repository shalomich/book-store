using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Various;

namespace Storage.Models
{
    public abstract class Product : Entity
    {
        private const int _minCost = 0;
        private const int _minQuantity = 0;

        private readonly static string _minCostMessage;
        private readonly static string _minQuantityMessage;

        private int _cost;
        private int _quantity;

        static Product() {
           _minCostMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, "Cost", _minCost.ToString());
           _minQuantityMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, "Quantity", _minQuantity.ToString());
        }
        public Product()
        {
            AddingDate = DateTime.Today;
        }

        public string Description { set; get; }
        public int Quantity 
        {
            set 
            {
                if (value < _minQuantity)
                    throw new ArgumentOutOfRangeException(_minQuantityMessage);
                _quantity = value;
            } 
            get 
            {
                return _quantity;
            } 
        }

        public int Cost
        {
            set
            {
                if (value < _minCost)
                    throw new ArgumentOutOfRangeException(_minCostMessage);
                _cost = value;
            }
            get
            {
                return _cost;
            }
        }
        public DateTime AddingDate {private set; get; }
    }
}
