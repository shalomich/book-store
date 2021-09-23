


using BookStore.Application.Dto;

namespace App.Areas.Store.ViewModels.Cards
{
    public record ProductCard
    {
        public int Id { init; get; }
        public string Name { init; get; }
        public int Cost { init; get; }
        public ImageDto TitleImage { init; get; }
    }
   
}
