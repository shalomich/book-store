using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Providers;
using BookStore.Application.Services;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.Services.Jwt;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueryWorker.Extensions;
using System.Reflection;

namespace BookStore.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration, Assembly externalAssembly)
    {
        var currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.Configure<S3Settings>(configuration.GetSection("S3"));
        services.Configure<MailSettings>(configuration.GetSection("Mail"));

        services.AddScoped<WebJwtParser>();
        services.AddScoped<TelegramBotJwtParser>();
        services.AddScoped<TokensFactory>();
        services.AddScoped<RefreshTokenRepository>();
        services.AddScoped<LoggedUserAccessor>();
        
        services.AddScoped<BattleSettingsProvider>();
 
        services.AddScoped<S3Storage>();
        services.AddScoped<ImageFileRepository>();

        services.AddScoped<EmailService>();

        services.AddDataTransformerBuildFacade(currentAssembly);
        services.AddScoped(typeof(DbEntityQueryBuilder<>));
        services.AddScoped(typeof(DbFormEntityQueryBuilder<>));

        services.AddMediatR(currentAssembly, externalAssembly);

        services.AddAutoMapper(currentAssembly, externalAssembly);

        return services;
    }
}