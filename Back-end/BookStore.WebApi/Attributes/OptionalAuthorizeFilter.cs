using BookStore.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Security.Claims;

namespace BookStore.WebApi.Attributes
{
    public class OptionalAuthorizeFilter : IAuthorizationFilter
    {
        private JwtParser JwtParser { get;}

        public OptionalAuthorizeFilter(JwtParser jwtParser)
        {
            JwtParser = jwtParser;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;

            if (httpContext.User.Identity.IsAuthenticated)
                return;

            if (!context.HttpContext.Request.Headers
                .TryGetValue("Authorization", out StringValues authorizationHeader))
                return;

            string token = authorizationHeader
                .ToString()
                .Replace("Bearer ", "");

            int userId;

            try
            {
                userId = JwtParser.FromToken(token);
            }
            catch (Exception)
            {
                return;
            }
            
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(nameof(userId), userId.ToString())
            }, "Custom");

            httpContext.User = new ClaimsPrincipal(identity);
        }
    }
}
