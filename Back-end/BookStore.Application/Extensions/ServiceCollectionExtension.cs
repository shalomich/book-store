using BookStore.Application.Services;
using BookStore.Application.Services.CatalogSelections;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<JwtParser>();
        services.AddScoped<TokensFactory>();
        services.AddScoped<RefreshTokenRepository>();
        services.AddScoped<LoggedUserAccessor>();

        services.AddScoped<SearchSelection>();
        services.AddScoped<CategorySelection>();
        services.AddScoped<SpecialForYouCategorySelection>();

        return services;
    }
}