using AutoMapperBuilder.Extensions.DependencyInjection;
using BookStore.Bot.Infrastructure;
using BookStore.Bot.Providers;
using BookStore.Bot.UseCases.Basket;
using BookStore.Bot.UseCases.Battle;
using BookStore.Bot.UseCases.Common;
using BookStore.Bot.UseCases.ViewSelection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Telegram.Bot;

namespace BookStore.Bot.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBotCore(this IServiceCollection services, 
        ConfigurationManager configuration) 
    {
       
        var currentAssembly = typeof(Program).Assembly;

        services.AddLogging(builder => builder.AddConsole());

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<TelegramBotDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });        
        
        services.AddScoped<CallbackCommandRepository>();
        services.AddScoped<CommandOrchestrator>();
        services.AddHostedService<BotHost>();

        services.AddAutoMapper(currentAssembly);
        services.AddAutoMapperBuilder(builder =>
        {
            builder.Profiles.Add(new SelectionMapperProfile(configuration));
            builder.Profiles.Add(new BasketMapperProfile(configuration));
            builder.Profiles.Add(new BattleMapperProfile(configuration));
        });

        services.AddMediatR(currentAssembly);

        services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(configuration["Token"]));

        services.Configure<BackEndSettings>(configuration.GetSection("BackEnd"));

        services.AddSingleton(new RestClient(configuration["BackEnd:ApiUri"])
            .UseNewtonsoftJson());
        services.AddScoped<AuthorizedRestClient>();
        services.AddScoped<UserProfileRestClient>();

        return services;
    }
}

