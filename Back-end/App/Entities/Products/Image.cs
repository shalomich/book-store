using App.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Various;

namespace App.Products.Entities
{
    public class Image
    {
        private string _format;

        public static readonly string[] FormatConsts = { "image/png", "image/jpeg" };

        private static readonly string _notExistFormatMessage;
        

        static Image()
        {
            _notExistFormatMessage = ExceptionMessages.GetMessage(ExceptionMessageType.NotExist, "Format", String.Join(" ", FormatConsts));
        }

        public int Id { set; get; }
        public string Name { set; get; }

        public string Data { set; get; }
        public string Format
        {
            set
            {
                if (FormatConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(_notExistFormatMessage);
                _format = value;
            }
            get
            {
                return _format;
            }
        }
  
        public virtual Album Album { set; get; }
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
