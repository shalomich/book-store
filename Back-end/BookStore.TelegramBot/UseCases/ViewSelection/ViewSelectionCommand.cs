using AutoMapper;
using BookStore.Application.Commands.Selection.Common;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookStore.TelegramBot.UseCases.ViewSelection;

internal record ViewSelectionCommand(Update Update) : TelegramBotCommand(Update);
internal class ViewSelectionCommandHandler : TelegramBotCommandHandler<ViewSelectionCommand>
{
    private ITelegramBotClient BotClient { get; }
    private IMapper Mapper { get; }
    private AuthorizedRestClient AuthorizedRestClient { get; }
    private BackEndSettings Settings { get; }

    public ViewSelectionCommandHandler(
        ITelegramBotClient botClient,
        IMapper mapper,
        IOptions<BackEndSettings> settingsOption,
        AuthorizedRestClient authorizedRestClient)
    {
        BotClient = botClient;
        Mapper = mapper;
        AuthorizedRestClient = authorizedRestClient;
        Settings = settingsOption.Value;
    }

    protected async override Task Handle(ViewSelectionCommand request, CancellationToken cancellationToken)
    {
        var provider = new ViewSelectionCommandProvider(request.Update);

        var (previewSet, successToGetSelection) = await TryGetSelectionAsync(provider, cancellationToken);

        if (!successToGetSelection)
        {
            return;
        }

        await ViewSelectionAsync(previewSet, provider, cancellationToken);
    }

    private async Task<(PreviewSetViewModel PreviewSet, bool SuccessToGetSelection)> TryGetSelectionAsync(
        ViewSelectionCommandProvider provider, CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Ищем комиксы по данной подборке...",
            cancellationToken: cancellationToken);

        var (selectionName, needToAutorize) = provider.GetSelectionName();
        var selectionPath = $"{Settings.SelectionPath}{selectionName}";

        var optionsParameters = provider.BuildPagging();

        PreviewSetDto previewSetDto;
        PreviewSetViewModel previewSetViewModel = null;

        var getSelectionRequest = new RestRequest(selectionPath)
            .AddParameter("Pagging.PageSize", optionsParameters.Pagging.PageSize)
            .AddParameter("Pagging.PageNumber", optionsParameters.Pagging.PageNumber);

        if (needToAutorize)
        {
            try
            {
                previewSetDto = await AuthorizedRestClient.SendRequestAsync<PreviewSetDto>(
                    telegramId: chatId,
                    requestFunction: (client, cancellationToken) => client.GetAsync(getSelectionRequest, cancellationToken),
                    cancellationToken: cancellationToken);
            }
            catch (InvalidOperationException exception)
            {
                await BotClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: exception.Message,
                    cancellationToken: cancellationToken);

                return (previewSetViewModel, false);
            }
        }
        else
        {
            previewSetDto = await AuthorizedRestClient.RestClient
                .GetAsync<PreviewSetDto>(getSelectionRequest, cancellationToken);
        }

        if (previewSetDto.TotalCount == 0)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: "К сожалению в данный момент нет комиксов по данной подборке.",
                cancellationToken: cancellationToken);

            return (previewSetViewModel, false);
        }

        previewSetViewModel = Mapper.Map<PreviewSetViewModel>(previewSetDto);

        return (previewSetViewModel, true);
    }

    private async Task ViewSelectionAsync(PreviewSetViewModel previewSet, ViewSelectionCommandProvider provider,
        CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        foreach (var preview in previewSet.Previews)
        {
            await BotClient.SendPhotoAsync(
                chatId: chatId,
                photo: preview.FileUrl,
                caption: provider.GetPreviewHtml(preview),
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
        }

        if (provider.TryGetNotEmptyNavigation(previewSet.TotalCount, out InlineKeyboardMarkup navigationKeyboard))
        {
            await BotClient.SendTextMessageAsync(
               chatId: chatId,
               text: "Ещё?",
               replyMarkup: navigationKeyboard,
               cancellationToken: cancellationToken);
        }
    }
}

