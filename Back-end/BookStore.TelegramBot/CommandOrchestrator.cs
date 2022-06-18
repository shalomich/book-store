using BookStore.TelegramBot.Commands.Help;
using BookStore.TelegramBot.Extensions;
using BookStore.TelegramBot.UseCases.Authenticate;
using BookStore.TelegramBot.UseCases.Battle;
using BookStore.TelegramBot.UseCases.Battle.CastVote;
using BookStore.TelegramBot.UseCases.Battle.ViewBattle;
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
        var commandName = update.TryGetCommand().Command;
        var chatId = update.GetChatId();

        if (await CommandNotExistAsync(commandName, botClient, chatId, cancellationToken))
        {
            return;
        }

        TelegramBotCommand telegramBotCommand = null;

        if (commandName.StartsWith(CommandNames.SelectionGroup))
        {
            telegramBotCommand = new ViewSelectionCommand(update);
        }
        else
        {
            telegramBotCommand = commandName switch
            {
                CommandNames.Start => new HelpCommand(update),
                CommandNames.Help => new HelpCommand(update),
                CommandNames.Authenticate => new AuthenticateCommand(update),
                CommandNames.ShowBattle => new ViewBattleCommand(update),
                CommandNames.CastVote => new CastVoteCommand(update),
                _ => throw new ArgumentOutOfRangeException(nameof(commandName))
            };
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

