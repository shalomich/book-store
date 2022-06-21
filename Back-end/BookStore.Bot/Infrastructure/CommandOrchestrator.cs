using BookStore.Bot.Commands.Help;
using BookStore.Bot.Extensions;
using BookStore.Bot.Providers;
using BookStore.Bot.UseCases.Authenticate;
using BookStore.Bot.UseCases.Basket.AddProductToBasket;
using BookStore.Bot.UseCases.Basket.ChangeBasketProductQuantity;
using BookStore.Bot.UseCases.Basket.CleanBasket;
using BookStore.Bot.UseCases.Basket.DeleteBasketProduct;
using BookStore.Bot.UseCases.Basket.ViewBasketProducts;
using BookStore.Bot.UseCases.Battle.CastVote;
using BookStore.Bot.UseCases.Battle.SpendVotingPoints;
using BookStore.Bot.UseCases.Battle.ViewBattle;
using BookStore.Bot.UseCases.Common;
using BookStore.Bot.UseCases.ViewSelection;
using BookStore.Bot.Infrastructure.CommandLineParsers;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.Bot.Infrastructure;
internal class CommandOrchestrator
{
    private IMediator Mediator { get; set; }
    private CallbackCommandRepository CallbackCommandRepository { get; }
    private ITelegramBotClient BotClient { get; }

    public CommandOrchestrator(
        IMediator mediator,
        CallbackCommandRepository callbackCommandRepository,
        ITelegramBotClient botClient
        )
    {
        Mediator = mediator;
        CallbackCommandRepository = callbackCommandRepository;
        BotClient = botClient;
    }

    public async Task Run(Update update, CancellationToken cancellationToken)
    {
        var (commandName, successToGetCommandName) = await TryGetCommandName(update, cancellationToken);

        if (!successToGetCommandName)
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
                CommandNames.SpendVotingPoints => new SpendVotingPointsCommand(update),
                CommandNames.ShowBasket => new ViewBasketProductsCommand(update),
                CommandNames.AddToBasket => new AddProductToBasketCommand(update),
                CommandNames.ChangeBasketProductQuantity => new ChangeBasketProductQuantityCommand(update),
                CommandNames.DeleteChoosen => new DeleteBasketProductCommand(update),
                CommandNames.DeleteAll => new CleanBasketCommand(update),
                _ => throw new ArgumentOutOfRangeException(nameof(commandName))
            };
        }

        await Mediator.Send(telegramBotCommand, cancellationToken);
    }

    private async Task<(string CommandName, bool SuccessToGetCommandName)> TryGetCommandName(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.GetChatId();

        var commandName = update.TryGetCommand().Command;

        if (CommandNames.All.Contains(commandName))
        {
            return (commandName, true);
        }

        var commandLine = await CallbackCommandRepository.GetAsync(chatId, cancellationToken);

        if (commandLine != null)
        {
            commandName = CommandLineParser.FromCommandLine(commandLine).Command;

            return (commandName, true);
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Нет такой команды",
            cancellationToken: cancellationToken
        );

        return (commandName, false);
    }
}

