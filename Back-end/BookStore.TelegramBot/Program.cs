using BookStore.Application.Extensions;
using BookStore.Persistance.Extensions;
using BookStore.TelegramBot.Notifications;
using BookStore.TelegramBot.UseCases.Common;
using BookStore.TelegramBot.UseCases.TryAuthenticateTelegramUser;
using BookStore.TelegramBot.UseCases.TryRegisterBotContact;
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
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;

            string command = null;

            bool isCommand = message.Text?.StartsWith("/") ?? false;

            if (isCommand)
            {
                var commandEndIndex = message.Text.IndexOf(" ");

                command = commandEndIndex == -1
                    ? message.Text.Substring(1)
                    : message.Text.Substring(1, commandEndIndex - 1);

            }

            bool isStartCommand = isCommand && command == Commands.Start;

            bool isConnectionSuccess = await TryConnectToStoreCommand(message, isStartCommand, botClient, cancellationToken);

            if (isConnectionSuccess == false || isCommand == false)
            {
                return;
            }

            await botClient.SendTextMessageAsync(message.Chat.Id, "Рандомная команда выполнена.");
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

    private async static Task<bool> TryConnectToStoreCommand(Message message, bool isStartCommand, ITelegramBotClient botClient, 
        CancellationToken cancellationToken)
    {
        var mediator = ServiceProvider.GetRequiredService<IMediator>();

        var registerStatus = await mediator.Send(new TryRegisterBotContactCommand(message, isStartCommand), cancellationToken);

        switch ((isStartCommand, registerStatus))
        {
            case (isStartCommand: true, registerStatus: RegisterBotContactStatus.Ready):

                await botClient.SendTextMessageAsync(message.Chat.Id, "Бот уже связан с онлайн магазином.");
                break;

            case (isStartCommand: true, registerStatus: RegisterBotContactStatus.Invalid):

                await botClient.SendTextMessageAsync(message.Chat.Id, "Необходимо залогиниться через сайт по ссылке: ");
                return false;

            case (isStartCommand: true, registerStatus: RegisterBotContactStatus.Success):

                await botClient.SendTextMessageAsync(message.Chat.Id, "Бот теперь связан с вашем аккаунтом на нашем сайте. Для аутентификации вашего телеграм аккаунта предоставьте номер телефона.");
                return false;

            case (isStartCommand: false, registerStatus: RegisterBotContactStatus.Invalid):

                await botClient.SendTextMessageAsync(message.Chat.Id, "Необходимо залогиниться через сайт по ссылке: ");
                return false;
        }

        var authenticateStatus = await mediator.Send(new TryAuthenticateTelegramUserCommand(message), cancellationToken);

        switch (authenticateStatus)
        {
            case AuthenticateTelegramUserStatus.Ready:
                break;

            case AuthenticateTelegramUserStatus.HasNoPhone:

                await botClient.SendTextMessageAsync(message.Chat.Id, "Для подтверждения аккаунта предоставьте номер телефона.");
                return false;

            case AuthenticateTelegramUserStatus.DifferentPhones:

                await botClient.SendTextMessageAsync(message.Chat.Id, "Номер в телеграм и в личном кабинете сайта отличаются.");
                return false;

            case AuthenticateTelegramUserStatus.Success:

                await botClient.SendTextMessageAsync(message.Chat.Id, "Телеграм аккаунт успешно аутентифицирован.");
                break;
        }

        return true;
    }
}
 

