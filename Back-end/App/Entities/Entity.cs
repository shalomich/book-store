using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public abstract class Entity
    {
        private const int _maxImageCount = 5;

        private List<Image> _images;
        public int Id { set; get; }
        public virtual string Name { set;get; }
        public string TitleImageName { set; get; }
        public List<Image> Images 
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
    }
}
