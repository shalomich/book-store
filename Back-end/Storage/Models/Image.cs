using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Image
    {
        private string _format;

        public static readonly string[] FormatConsts = { "image/png", "image/jpeg" };
        public int Id { set; get; }
        public string Name { set; get; }
        public string Format
        {
            set
            {
                if (FormatConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException($"{value} не формат цифрового изображения");
                _format = value;
            }
            get
            {
                return _format;
            }
        }
        public string Endoding { set; get; }
        public string Data { set; get; }
    }
}
