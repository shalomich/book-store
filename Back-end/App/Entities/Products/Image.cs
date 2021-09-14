using App.Entities;
using App.Entities.Products;
using System;
using System.Linq;


namespace App.Products.Entities
{
    public class Image : IEntity
    {
        private string _format;

        public static readonly string[] FormatConsts = { "image/png", "image/jpeg" };

        public int Id { set; get; }
        public string Name { set; get; }

        public string Data { set; get; }
        public string Format
        {
            set
            {
                if (FormatConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException();
                _format = value;
            }
            get
            {
                return _format;
            }
        }
  
        public Album Album { set; get; }
        public int AlbumId { set; get; }

        public override bool Equals(object obj)
        {
            return obj is Image image &&
                   Name == image.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
