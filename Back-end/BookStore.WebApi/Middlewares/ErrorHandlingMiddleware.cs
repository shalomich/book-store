using System;
using System.Net;
using System.Threading.Tasks;
using BookStore.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookStore.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string errorMessage;

            if (exception is RestException restException)
            {
                context.Response.StatusCode = (int)restException.Code;
                errorMessage = restException.Message;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorMessage = exception.ToString();   
            }

            context.Response.ContentType = "appliation/json";

            string responce = JsonConvert.SerializeObject(new { errors = new { Server = new string[] { errorMessage } } });

            await context.Response.WriteAsync(responce);
        }
    }
}
