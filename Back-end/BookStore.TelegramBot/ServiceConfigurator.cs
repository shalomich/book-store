using AutoMapperBuilder.Extensions.DependencyInjection;
using BookStore.TelegramBot.Controllers;
using BookStore.TelegramBot.Notifications;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Basket;
using BookStore.TelegramBot.UseCases.Battle;
using BookStore.TelegramBot.UseCases.Common;
using BookStore.TelegramBot.UseCases.ViewSelection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Telegram.Bot;

namespace BookStore.TelegramBot;
internal static class ServiceConfigurator
{
    public static IServiceProvider ConfigureServices()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var currentAssembly = typeof(Program).Assembly;

        var services = new ServiceCollection()
            .AddSingleton(configuration)
            .AddLogging(builder => builder.AddConsole());

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<TelegramBotDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddAutoMapper(currentAssembly);
        services.AddAutoMapperBuilder(builder =>
        {
            builder.Profiles.Add(new SelectionMapperProfile(configuration));
            builder.Profiles.Add(new BasketMapperProfile(configuration));
            builder.Profiles.Add(new BattleMapperProfile(configuration));
        });
        services.AddMediatR(currentAssembly);

        services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(configuration["Token"]));

        services.AddScoped<CommandOrchestrator>();
        services.AddScoped<CallbackCommandRepository>();

        services.Configure<TelegramBotMessages>(configuration.GetSection("Messages"));
        services.Configure<BackEndSettings>(configuration.GetSection("BackEnd"));

        services.AddSingleton(new RestClient(configuration["BackEnd:ApiUri"])
            .UseNewtonsoftJson());
        services.AddScoped<AuthorizedRestClient>();
        services.AddScoped<UserProfileRestClient>();

        return services.BuildServiceProvider();
    }
}

