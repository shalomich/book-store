using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Image
    {
        private string _format;
        private string _encoding;

        public static readonly string[] FormatConsts = { "image/png", "image/jpeg" };

        public static readonly string[] EncodingConsts = { "base64" };

        private static readonly string _notExistFormatMessage = "{0} не соотвествует нужному формату {1}";
        private static readonly string _notExistEncodingMessage = "{0} не соотвествует нужной кодировке {1}";
        public int Id { set; get; }
        public string Name { set; get; }
        public string Format
        {
            set
            {
                if (FormatConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(String.Format(_notExistFormatMessage, value, FormatConsts));
                _format = value;
            }
            get
            {
                return _format;
            }
        }
        public string Encoding 
        { 
            set 
            {
                if (EncodingConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(String.Format(_notExistEncodingMessage, value, EncodingConsts));
                _encoding = value;
            } 
            get 
            {
                return _encoding;
            } 
        }
        public string Data { set; get; }
    }
}
