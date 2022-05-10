using BookStore.Application.Extensions;
using BookStore.Persistance.Extensions;
using BookStore.TelegramBot.Notifications;
using BookStore.TelegramBot.UseCases.AuthenticateTelegramBotContact;
using BookStore.TelegramBot.UseCases.Common;
using MediatR;
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

            string command = null;

            bool isCommand = message.Text.StartsWith("/");

            if (isCommand)
            {
                var commandEndIndex = message.Text.IndexOf(" ");

                command = commandEndIndex == -1
                    ? message.Text.Substring(1)
                    : message.Text.Substring(1, commandEndIndex + 1);

            }

            bool isStartCommand = isCommand && command == Commands.Start;

            bool isConnectionSuccess = await mediator.Send(new TryConnectToStoreCommand(message, isStartCommand), cancellationToken);

            if (!isConnectionSuccess && !isCommand)
            {
                return;
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
 

