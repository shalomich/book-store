using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Products;
using System;
using System.Linq;


namespace BookStore.Domain.Entities.Products
{
    public class Image : IEntity
    {
        public const int MinHeight = 1;
        public const int MinWidth = 1;

        private string _format;
        private int _height;
        private int _width;

        public static readonly string[] FormatConsts = { "image/png", "image/jpeg" };

        public int Id { set; get; }
        public string Name { set; get; }

        public int Height
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();
                _height = value;
            }

            get
            {
                return _height;
            }
        }

        public int Width
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();
                _width = value;
            }

            get
            {
                return _width;
            }
        }

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
