using AutoMapper;
using BookStore.Application.Queries.Battle.GetBattleInfo;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookStore.TelegramBot.UseCases.Battle.ViewBattle;

internal record ViewBattleCommand(Update Update) : TelegramBotCommand(Update);

internal class ViewBattleCommandHandler : TelegramBotCommandHandler<ViewBattleCommand>
{
    private RestClient RestClient { get; }
    private IMapper Mapper { get; }
    public ITelegramBotClient BotClient { get; }
    private BackEndSettings Settings { get; }
    public ViewBattleCommandHandler(
        RestClient restClient,
        IMapper mapper,
        IOptions<BackEndSettings> settingsOption,
        ITelegramBotClient botClient)
    {
        RestClient = restClient;
        Mapper = mapper;
        BotClient = botClient;
        Settings = settingsOption.Value;
    }


    protected override async Task Handle(ViewBattleCommand request, CancellationToken cancellationToken)
    {
        var update = request.Update;
        var chatId = update.Message.Chat.Id;

        var provider = new ViewBattleCommandProvider(request.Update);

        var battleInfoDto = await RestClient.GetAsync<BattleInfoDto>(new RestRequest(Settings.BattleInfoPath), cancellationToken);

        var battleInfoViewModel = Mapper.Map<BattleInfoViewModel>(battleInfoDto);

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: provider.GetBattleInfoHtml(battleInfoViewModel),
            parseMode: ParseMode.Html,
            cancellationToken: cancellationToken);

        await BotClient.SendPhotoAsync(
            chatId: chatId,
            photo: battleInfoViewModel.FirstBattleBook.FileUrl,
            caption: provider.GetBattleBookHtml(battleInfoViewModel.FirstBattleBook, battleInfoViewModel),
            parseMode: ParseMode.Html,
            cancellationToken: cancellationToken);

        await BotClient.SendPhotoAsync(
            chatId: chatId,
            photo: battleInfoViewModel.SecondBattleBook.FileUrl,
            caption: provider.GetBattleBookHtml(battleInfoViewModel.SecondBattleBook, battleInfoViewModel),
            parseMode: ParseMode.Html,
            cancellationToken: cancellationToken);

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Выберите комикс.",
            replyMarkup: provider.GetBattleBookToChoiceButtons(battleInfoViewModel),
            cancellationToken: cancellationToken);
    }
}

