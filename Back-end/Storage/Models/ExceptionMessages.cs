using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public static class ExceptionMessages
    {
        public enum MessageType
        {
            NotExist,
            Invalid,
            Less,
            More
        }

        private static Dictionary<MessageType, string> _messageSchemas => new Dictionary<MessageType, string>()
        {
            {MessageType.NotExist, "Not exist this {0}, choose from {1}"},
            {MessageType.Invalid, "Invalid value for {0}, use this schema: {1}"},
            {MessageType.Less, "Current {0} less than minimum ({1})" },
            {MessageType.More, "Current {0} more than maximum ({1})"}
        };

        public static string GetMessage(MessageType type, string property, string correctValue)
        {
            var messageSchema = _messageSchemas[type];
            return String.Format(messageSchema, property, correctValue);
        }
        
    }
}
