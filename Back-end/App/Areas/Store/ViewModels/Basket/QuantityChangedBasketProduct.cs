using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels.Basket
{
    public record QuantityChangedBasketProduct
    {
        [Range(1,int.MaxValue)]
        public int Quantity { init; get; }
    }
 
}
