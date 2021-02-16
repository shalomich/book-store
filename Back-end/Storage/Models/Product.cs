using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Product : Entity
    {
        private const int _minCost = 0;
        private const int _minQuantity = 0;

        private readonly static string _minCostMessage = $"Цена не должна быть меньше {_minCost}";
        private readonly static string _minQuantityMessage = $"Количество не должно быть меньше {_minQuantity}";

        private int _cost;
        private int _quantity;
        public Product()
        {
            AddingDate = DateTime.Today;
        }
        public string Name { set; get; }
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
        public string Description { set; get; }
        public DateTime AddingDate {private set; get; }
    }
}
