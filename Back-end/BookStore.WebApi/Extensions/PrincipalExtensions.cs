using BookStore.Domain.Entities;
using System.Security.Claims;

namespace BookStore.WebApi.Extensions
{
    public static class PrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal
                .FindFirst(nameof(User.Id))
                .Value);
        }
    }
}
