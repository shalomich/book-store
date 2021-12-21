using BookStore.Domain.Entities;
using System.Security.Claims;

namespace BookStore.WebApi.Extensions
{
    public static class PrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            int userId = int.Parse(principal
                .FindFirst(nameof(userId))
                .Value);

            return userId;
        }
    }
}
