using BookStore.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using BookStore.TelegramBot.UseCases.Common;

namespace BookStore.TelegramBot.Commands.Help;
internal class HelpCommand
{
    private ITelegramBotClient BotClient { get; }

    public HelpCommand(ITelegramBotClient botClient)
    {
        BotClient = botClient;
    }
    public async Task Help(Update update, CancellationToken cancellationToken)
    {
        var commandList = CommandNames.All
                .Except(new string[] { CommandNames.Start })
                .Aggregate(string.Empty, (commands, command) => commands + $"\n /{command}");

        var commandsMessage = "Команды:\n" + commandList;

        await BotClient.SendTextMessageAsync(
           chatId: update.GetChatId(),
           text: commandsMessage,
           cancellationToken: cancellationToken
        );
    }
}

