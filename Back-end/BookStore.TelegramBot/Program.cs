using BookStore.TelegramBot.UseCases.RegisterTelegramBotContact;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookStore.TelegramBot;

class Program
{
    private static ITelegramBotClient BotClient { set; get; }
    private static IMediator Mediator { set; get; }

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;

            if (message.Text.StartsWith("/"))
            {
                var commandEndIndex = message.Text.IndexOf(" ");

                var command = commandEndIndex == -1
                    ? message.Text.Substring(1)
                    : message.Text.Substring(1, commandEndIndex);

                IRequest request = command switch
                {
                    "start" => new RegisterTelegramBotContactCommand(message),
                    _ => throw new ArgumentOutOfRangeException()
                };

                await Mediator.Send(request);
            }
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(JsonConvert.SerializeObject(exception));
    }


    static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }
        };

        BotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
}

