using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public abstract class Entity
    {
        public int Id { set; get; }
        public string TitleImageName { set; get; }
        public List<Image> Images { set; get; }
    }
}
