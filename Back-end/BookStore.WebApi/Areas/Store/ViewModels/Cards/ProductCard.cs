
using BookStore.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.ViewModels.Cards
{
    public abstract record ProductCard
    {
        public int Id { init; get; }
        public string Name { init; get; }
        public int Cost { init; get; }
        public ImageDto TitleImage { init; get; }
        public int Quantity { init; get; }
        public string Description { init; get; }
        public ISet<ImageDto> NotTitleImages { init; get; }
    }
}
