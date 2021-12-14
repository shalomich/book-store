using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Providers
{
    public record JwtSettings
    {
        public string TokenKey { init; get; }
        public string AppTokenProvider { init; get; }
        public int ExpiredMinutes { init; get; }

        public void Deconstruct(out string tokenKey, out int expiredMinutes, out string appTokenProvider)
        {
            tokenKey = TokenKey;
            appTokenProvider = AppTokenProvider;
            expiredMinutes = ExpiredMinutes;
        }
    }
}
