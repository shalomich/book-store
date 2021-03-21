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

        private static readonly string _notExistFormatMessage;
        private static readonly string _notExistEncodingMessage;
        public int Id { set; get; }
        public string Name { set; get; }

        static Image()
        {

            _notExistFormatMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.NotExist, "Format", String.Join(" ", FormatConsts));
            _notExistEncodingMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.NotExist, "Encoding", String.Join(" ", EncodingConsts));
        }

        public Entity Entity { set; get; }
        public int EntityId { set; get; }
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
        public string Encoding 
        { 
            set 
            {
                if (EncodingConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(_notExistEncodingMessage);
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
