using BookStore.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using BookStore.TelegramBot.UseCases.Common;

namespace BookStore.TelegramBot.Commands.Help;

internal record HelpCommand(Update Update) : TelegramBotCommand(Update);
internal class HelpCommandHandler : TelegramBotCommandHandler<HelpCommand>
{
    private ITelegramBotClient BotClient { get; }

    public HelpCommandHandler(ITelegramBotClient botClient)
    {
        BotClient = botClient;
    }

    protected async override Task Handle(HelpCommand request, CancellationToken cancellationToken)
    {
        var commandList = CommandNames.All
                .Except(CommandNames.NotForUser)
                .Aggregate(string.Empty, (commands, command) => commands + $"\n /{command}");

        var commandsMessage = "Команды:\n" + commandList;

        await BotClient.SendTextMessageAsync(
           chatId: request.Update.GetChatId(),
           text: commandsMessage,
           cancellationToken: cancellationToken
        );
    }
}

