using System;
using System.Security.Claims;

namespace BookStore.Application.Extensions;
public static class PrincipalExtensions
{
    public static int GetCurrentUserId(this ClaimsPrincipal principal)
    {
        int userId;

        try
        {
            var userIdString = principal.FindFirst(nameof(userId)).Value;

            userId = int.Parse(userIdString);
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException("Can not get id of not authenticated user.", exception);
        }

        return userId;
    }
}

