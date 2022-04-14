using Microsoft.AspNetCore.Http;

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

        var userIdString = HttpContextAccessor.HttpContext
            .User.FindFirst(nameof(userId)).Value;
            
        userId = int.Parse(userIdString);

        return userId;
    }

    public bool IsAuthenticated()
    {
        return HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
