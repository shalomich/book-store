using BookStore.TelegramBot.Controllers;
using BookStore.TelegramBot.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot;

class Program
{
    private static IServiceProvider ServiceProvider { set; get; }

    static void Main(string[] args)
    {
        ServiceProvider = ServiceConfigurator.ConfigureServices();

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
        if (update.IsCommand())
        {
            var orchestrator = ServiceProvider.GetRequiredService<CommandOrchestrator>();

            await orchestrator.Run(update, botClient, cancellationToken);
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(JsonConvert.SerializeObject(exception));
    }
}
 

