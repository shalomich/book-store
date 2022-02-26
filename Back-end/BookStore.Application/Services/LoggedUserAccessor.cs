using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
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
}
