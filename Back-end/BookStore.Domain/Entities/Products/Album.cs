
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Products
{
    public class Album : IEntity
    {
        private const int _maxImageCount = 5;

        private ISet<Image> _images;

        public int Id { set; get; }
        public string TitleImageName { set; get; }
        public ISet<Image> Images
        {
            set
            {
                if (value.Count > _maxImageCount)
                    throw new ArgumentException();
                _images = value;
            }

            get
            {
                return _images;
            }
        }

        public Image TitleImage => Images?.Single(image => image.Name == TitleImageName) 
            ?? Images.FirstOrDefault();

        public ISet<Image> NotTitleImages => Images
            .Where(image => image != TitleImage)
            .ToHashSet();
        public virtual Product Product { set; get; }
        public int ProductId { set; get; }
    }
}
