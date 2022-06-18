using AutoMapper;
using BookStore.Application.Commands.Selection.Common;
using BookStore.TelegramBot.Extensions;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookStore.TelegramBot.UseCases.ViewSelection;

internal record ViewSelectionCommand(Update Update) : TelegramBotCommand(Update);
internal class ViewSelectionCommandHandler : TelegramBotCommandHandler<ViewSelectionCommand>
{
    private ITelegramBotClient BotClient { get; }
    private IMapper Mapper { get; }
    private RestClient RestClient { get; }
    private BackEndSettings Settings { get; }

    public ViewSelectionCommandHandler(
        ITelegramBotClient botClient,
        IMapper mapper,
        IOptions<BackEndSettings> settingsOption,
        RestClient restClient)
    {
        BotClient = botClient;
        Mapper = mapper;
        RestClient = restClient;
        Settings = settingsOption.Value;
    }

    protected async override Task Handle(ViewSelectionCommand request, CancellationToken cancellationToken)
    {
        var update = request.Update;
        var chatId = update.GetChatId();

        var provider = new ViewSelectionCommandProvider(update);
        var selectionPath = $"{Settings.SelectionPath}{provider.GetSelectionName()}";

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Ищем комиксы по данной подборке...",
            cancellationToken: cancellationToken);

        var optionsParameters = provider.BuildPagging();

        var previewSet = await RestClient.GetAsync<PreviewSetDto>(new RestRequest(selectionPath)
            .AddParameter("Pagging.PageSize", optionsParameters.Pagging.PageSize)
            .AddParameter("Pagging.PageNumber", optionsParameters.Pagging.PageNumber), 
            cancellationToken);

        if (previewSet.TotalCount == 0)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: "К сожалению в данный момент нет комиксов по данной подборке.",
                cancellationToken: cancellationToken);

            return;
        }

        var telegramBookPreviews = previewSet.Previews
            .Select(preview => Mapper.Map<PreviewViewModel>(preview))
            .ToList();

        foreach (var preview in telegramBookPreviews)
        {
            await BotClient.SendPhotoAsync(
                chatId: chatId,
                photo: preview.FileUrl,
                caption: provider.GetPreviewHtml(preview),
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Ещё?",
            replyMarkup: provider.BuildNavigationButtons(previewSet.TotalCount),
            cancellationToken: cancellationToken);
    }
}

