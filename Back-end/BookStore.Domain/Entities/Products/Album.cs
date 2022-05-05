
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Products
{
    public class Album : IEntity
    {
        public const int MinImageCount = 1;

        public const int MaxImageCount = 5;

        public int Id { set; get; }
        public string TitleImageName { set; get; }
        public ISet<Image> Images { set; get; }
        public Image TitleImage => Images?.Single(image => image.Name == TitleImageName);

        public ISet<Image> NotTitleImages => Images
            ?.Where(image => image != TitleImage)
            ?.ToHashSet();
        public virtual Product Product { set; get; }
        public int ProductId { set; get; }
    }
}
