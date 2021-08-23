using App.Areas.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels.Basket
{
    public record BasketProductDto
    {
        public string Name { init; get; }
        public int Cost { init; get; }
        public int Quantity { init; get; }
        public ImageDto TitleImage {init; get;}
    }
}
