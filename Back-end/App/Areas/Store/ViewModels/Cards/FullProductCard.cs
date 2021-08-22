using App.Areas.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels.Cards
{
    public abstract record FullProductCard : ProductCard
    {
        public uint Quantity { init; get; }
        public string Description { init; get; }
        public ISet<ImageDto> NotTitleImages { init; get; }
    }
}
