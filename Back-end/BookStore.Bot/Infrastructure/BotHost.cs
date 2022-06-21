using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace BookStore.Bot.Infrastructure;
internal class BotHost : IHostedService
{
    private ITelegramBotClient BotClient { get; }
    private IServiceProvider ServiceProvider { get; }
    public ILogger<BotHost> Logger { get; }

    public BotHost(
        ITelegramBotClient botClient,
        IServiceProvider serviceProvider,
        ILogger<BotHost> logger
    )
    {
        BotClient = botClient;
        ServiceProvider = serviceProvider;
        Logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        BotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions(),
            cancellationToken
        );

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        using (IServiceScope scope = ServiceProvider.CreateScope())
        {
            var orchestrator = scope.ServiceProvider.GetRequiredService<CommandOrchestrator>();

            try
            {
                await orchestrator.Run(update, cancellationToken);
            }
            catch(Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception.");
            }
        }
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(JsonConvert.SerializeObject(exception));
    }
}
