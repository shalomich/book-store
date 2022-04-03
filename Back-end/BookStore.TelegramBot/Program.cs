using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Passport.Request;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookStore.TelegramBot;

internal class Program
{
    private static ITelegramBotClient BotClient { get; } = new TelegramBotClient("5298206558:AAE3BhhtWnrQgDJSaDzoZ6-FZpiIWJsFUrw");
    public static void Main()
    {
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };

        BotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        PassportScope scope = new PassportScope(new[]
        {
            new PassportScopeElementOne(PassportEnums.Scope.PhoneNumber),
        });

        int botId = (int)BotClient.BotId;

        var authReq = new AuthorizationRequestParameters(
            botId: botId,
            publicKey: PublicKey,
            nonce: "Subscription confirmation",
            scope: scope);

        var query = authReq.Query.Replace(botId.ToString(), BotClient.BotId.ToString());
        
        var userId = update.Message.From.Id;

        await BotClient.SendTextMessageAsync(
            userId,
            "Registration",
            ParseMode.Markdown,
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton.WithUrl(
                    "Share via Passport",
                    $"http://localhost:4200/book-store?{query}"
                )
        );
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }

    private const string PublicKey = "-----BEGIN PUBLIC KEY-----MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFwIDAQAB-----END PUBLIC KEY-----";
}

