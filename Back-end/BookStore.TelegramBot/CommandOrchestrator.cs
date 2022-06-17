using BookStore.TelegramBot.Commands.Help;
using BookStore.TelegramBot.Extensions;
using BookStore.TelegramBot.UseCases.Common;
using BookStore.TelegramBot.UseCases.ViewSelection;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.Controllers;
internal class CommandOrchestrator
{
    private IMediator Mediator { get; set; }
    public CommandOrchestrator(
        IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task Run(Update update, ITelegramBotClient botClient,
        CancellationToken cancellationToken)
    {
        var command = update.TryGetCommand().Command;
        var chatId = update.GetChatId();

        if (await CommandNotExistAsync(command, botClient, chatId, cancellationToken))
        {
            return;
        }

        TelegramBotCommand telegramBotCommand = null;
        
        if (command == CommandNames.Start || command == CommandNames.Help)
        {
            telegramBotCommand = new HelpCommand(update);
        }
        else if (command.StartsWith(CommandNames.SelectionGroup))
        {
            telegramBotCommand = new ViewSelectionCommand(update);
        }
        else
        {
            throw new InvalidOperationException();
        }

        await Mediator.Send(telegramBotCommand, cancellationToken);
    }

    private async Task<bool> CommandNotExistAsync(string command, ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        if (CommandNames.All.Contains(command))
        {
            return false;
        }

        await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Нет такой команды",
                cancellationToken: cancellationToken
        );

        return true;
    }
}

