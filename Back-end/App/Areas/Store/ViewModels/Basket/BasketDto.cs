using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels.Basket
{
    public record BasketDto
    {
        public int TotalAmount { init; get; }
        public int TotalQuantity { init; get; }
        public BasketProductDto[] BasketProducts { init; get; }
    }
}
