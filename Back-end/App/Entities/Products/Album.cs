using App.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Products
{
    public class Album
    {
        private const int _maxImageCount = 5;

        private ISet<Image> _images;

        public int Id { set; get; }
        public string TitleImageName { set; get; }
        public virtual ISet<Image> Images
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
        public virtual Product Product { set; get; }
        public int ProductId { set; get; }
    }
}
