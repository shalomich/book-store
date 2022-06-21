using BookStore.Bot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using BookStore.Bot.UseCases.Common;
using BookStore.Bot.Providers;

namespace BookStore.Bot.Commands.Help;

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
        var commandList = CommandNames.GetUserAvailableCommands()
                .Aggregate(string.Empty, (commands, command) => commands + $"\n /{command.CommandName} - {command.Description}");

        var commandsMessage = "Команды:\n" + commandList;

        await BotClient.SendTextMessageAsync(
           chatId: request.Update.GetChatId(),
           text: commandsMessage,
           cancellationToken: cancellationToken
        );
    }
}

