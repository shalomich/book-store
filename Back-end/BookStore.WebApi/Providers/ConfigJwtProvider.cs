using BookStore.Application.Providers;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStore.WebApi.Providers
{
    public class ConfigJwtProvider : IJwtProvider
    {
        public IConfiguration Configuration { get; }

        public ConfigJwtProvider(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public JwtSettings GenerateSettings()
        {
            return Configuration
                .GetSection("JWT")
                .Get<JwtSettings>();
        }
    }
}
