using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Products
{
    public class ProductCloseout : IEntity
    {
        private DateTime? _replenishmentDate;
        public int Id { set; get; }
        public DateTime Date { set; get; }
        public DateTime? ReplenishmentDate 
        { 
            set
            {
                if (value < Date)
                    throw new ArgumentOutOfRangeException();

                _replenishmentDate = value;
            }
            get
            {
                return _replenishmentDate;
            }
        }
        public Product Product { set; get; }
        public int ProductId { set; get; }
    }
}
