using AutoMapper;
using BookStore.TelegramBot.Commands.Help;
using BookStore.TelegramBot.Commands.Selection;
using BookStore.TelegramBot.Extensions;
using BookStore.TelegramBot.UseCases.Common;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.Controllers;
internal class CommandOrchestrator
{
    private IMediator Mediator { get; set; }
    private IMapper Mapper { get; }

    public CommandOrchestrator(
        IMediator mediator,
        IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
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

        if (command == CommandNames.Start)
        {
            command = CommandNames.Help;
        }

        Func<Update, CancellationToken, Task> commandExpression = command switch
        {
            CommandNames.Help => new HelpCommand(botClient).Help,
            CommandNames.Novelties => new SelectionCommandGroup(Mediator, botClient, Mapper).GetNoveltySelection,
            CommandNames.GoneOnSale => new SelectionCommandGroup(Mediator, botClient, Mapper).GetGoneOnSaleSelection,
            CommandNames.BackOnSale => new SelectionCommandGroup(Mediator, botClient, Mapper).GetBackOnSaleSelection,
            CommandNames.CurrentDayAuthor => new SelectionCommandGroup(Mediator, botClient, Mapper).GetCurrentDayAuthorSelection,
            CommandNames.Popular => new SelectionCommandGroup(Mediator, botClient, Mapper).GetPopularSelection
        };

        await commandExpression(update, cancellationToken);
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

