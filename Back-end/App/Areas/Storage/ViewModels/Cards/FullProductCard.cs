using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels.Cards
{
    public abstract record FullProductCard : ProductCard
    {
        public uint Quantity { init; get; }
        public string Description { init; get; }
        public ISet<ImageDto> NotTitleImages { init; get; }
    }
}
