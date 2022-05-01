using Microsoft.AspNetCore.Http;
using System;

namespace BookStore.Application.Services;
public class LoggedUserAccessor
{
    private IHttpContextAccessor HttpContextAccessor { get;}

    public LoggedUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    public int GetCurrentUserId()
    {
        int userId;

        try
        {
            var userIdString = HttpContextAccessor.HttpContext.User.FindFirst(nameof(userId)).Value;

            userId = int.Parse(userIdString);
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException("Can not get id of not authenticated user.", exception);
        }
        
        return userId;
    }

    public bool IsAuthenticated()
    {
        return HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
