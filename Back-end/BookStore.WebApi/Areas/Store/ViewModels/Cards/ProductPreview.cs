
using BookStore.Application.Dto;

namespace BookStore.WebApi.Areas.Store.ViewModels.Cards
{
    public abstract record ProductPreview
    {
        public int Id { init; get; }
        public string Name { init; get; }
        public int Cost { init; get; }
        public ImageDto TitleImage { init; get; }
    }
   
}
