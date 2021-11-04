
using BookStore.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.ViewModels.Cards
{
    public abstract record FullProductCard : ProductCard
    {
        public int Quantity { init; get; }
        public string Description { init; get; }
        public ISet<ImageDto> NotTitleImages { init; get; }
    }
}
