using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class RefreshTokenRepository
    {
        private const string refreshTokenName = "RefreshToken";
        private string AppTokenProvider { get; } 
        private UserManager<User> UserManager { get; }

        public RefreshTokenRepository(UserManager<User> userManager, IConfiguration configuration)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            AppTokenProvider = configuration[nameof(AppTokenProvider)];
        }

        public async Task<string> Create(User user)
        {
            string refreshToken = await UserManager.GenerateUserTokenAsync(user, AppTokenProvider, refreshTokenName);
            await UserManager.SetAuthenticationTokenAsync(user, AppTokenProvider, refreshTokenName, refreshToken);

            return refreshToken;
        }
        public async Task<bool> IsValid(string refreshToken, User user)
        {
            string currentRefreshToken = await UserManager.GetAuthenticationTokenAsync(user, AppTokenProvider, refreshTokenName);

            return currentRefreshToken == refreshToken;
        }

        public Task Remove(User user)
        {
            return UserManager.RemoveAuthenticationTokenAsync(user, AppTokenProvider, refreshTokenName);
        }
    }
}
