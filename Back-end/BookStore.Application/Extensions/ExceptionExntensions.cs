using System;

namespace BookStore.Application.Extensions;
public static class ExceptionExtensions
{
    public static string GetFullMessage(this Exception exception)
    {
        string message = exception.Message;

        while (exception.InnerException != null)
        {
            exception = exception.InnerException;

            if (!string.IsNullOrWhiteSpace(exception.Message))
            {
                message += " " + exception.Message;
            }
        }

        return message;
    }
}