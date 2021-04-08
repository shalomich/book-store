using System;
using System.Collections.Generic;

namespace Various
{
    public enum ExceptionMessageType
    {
        NotExist,
        Invalid,
        Less,
        More,
        Null,
        MustHave
    }
    public static class ExceptionMessages
    {
        private static Dictionary<ExceptionMessageType, string> _messageSchemas => new Dictionary<ExceptionMessageType, string>()
        {
            {ExceptionMessageType.NotExist, "Not exist this {0}, choose from ({1})"},
            {ExceptionMessageType.Invalid, "Invalid value for {0}, used {1}"},
            {ExceptionMessageType.Less, "Current {0} less than minimum ({1})" },
            {ExceptionMessageType.More, "Current {0} more than maximum ({1})"},
            {ExceptionMessageType.Null,"Current {0} can't be null, example ({1})"},
            {ExceptionMessageType.MustHave, "Current {0} must have {1}"}
        };

        public static string GetMessage(ExceptionMessageType type, string property, string correctValue)
        {
            var messageSchema = _messageSchemas[type];
            return String.Format(messageSchema, property, correctValue);
        }

    }
}
