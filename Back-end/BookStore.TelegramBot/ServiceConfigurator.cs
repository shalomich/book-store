﻿using BookStore.TelegramBot.Controllers;
using BookStore.TelegramBot.Notifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

        services.AddAutoMapper(currentAssembly);
        services.AddMediatR(currentAssembly);

        services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(configuration["Token"]));

        services.AddScoped<CommandOrchestrator>();

        services.Configure<TelegramBotMessages>(configuration.GetSection("Messages"));

        return services.BuildServiceProvider();
    }
}

