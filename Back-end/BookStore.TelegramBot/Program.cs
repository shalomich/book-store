using BookStore.Application.Extensions;
using BookStore.Persistance.Extensions;
using BookStore.TelegramBot.Notifications;
using BookStore.TelegramBot.UseCases.RegisterTelegramBotContact;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookStore.TelegramBot;

class Program
{
    private static IServiceProvider ServiceProvider { set; get; }
    
    static void Main(string[] args)
    {
        ServiceProvider = ConfigureServices();

        var botClient = ServiceProvider.GetRequiredService<ITelegramBotClient>();
        
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions(),
            new CancellationTokenSource().Token
        );
        Console.ReadLine();
    }

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var mediator = ServiceProvider.GetRequiredService<IMediator>();

        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;

            if (message.Text.StartsWith("/"))
            {
                var commandEndIndex = message.Text.IndexOf(" ") - 1;

                var command = commandEndIndex == -1
                    ? message.Text.Substring(1)
                    : message.Text.Substring(1, commandEndIndex);

                IRequest request = command switch
                {
                    "start" => new RegisterTelegramBotContactCommand(message),
                    _ => throw new ArgumentOutOfRangeException()
                };

                await mediator.Send(request, cancellationToken);
            }
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(JsonConvert.SerializeObject(exception));
    }

    private static IServiceProvider ConfigureServices()
    {
        var settingsPath = Path.GetFullPath(Path.Combine(@"../../../../BookStore.WebApi/appsettings.json"));

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsPath, optional: true, reloadOnChange: true)
            .Build();

        var currentAssembly = typeof(Program).Assembly;

        var services = new ServiceCollection()
            .AddPersistance(configuration)
            .AddApplicationCore(configuration, currentAssembly)
            .AddSingleton(configuration)
            .AddLogging(builder => builder.AddConsole());

        services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(configuration["TelegramBot:Token"]));

        services.Configure<TelegramBotMessages>(configuration.GetSection("TelegramBot:Messages"));

        return services.BuildServiceProvider();
    }
}

