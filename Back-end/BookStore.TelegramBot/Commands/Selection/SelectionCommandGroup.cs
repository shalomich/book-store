using AutoMapper;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Commands.Selection.GetBackOnSaleSelection;
using BookStore.Application.Commands.Selection.GetCurrentDayAuthorSelection;
using BookStore.Application.Commands.Selection.GetGoneOnSaleSelection;
using BookStore.Application.Commands.Selection.GetNoveltySelection;
using BookStore.Application.Dto;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookStore.TelegramBot.Commands.Selection;


internal class SelectionCommandGroup
{
    private IMediator Mediator { get; }
    private ITelegramBotClient BotClient { get; }
    private IMapper Mapper { get; }

    public SelectionCommandGroup(
        IMediator mediator,
        ITelegramBotClient botClient,
        IMapper mapper)
    {
        Mediator = mediator;
        BotClient = botClient;
        Mapper = mapper;
    }

    public async Task GetNoveltySelection(Update update, CancellationToken cancellationToken)
    {
        var provider = new SelectionMetadataProvider(update);

        Func<OptionParameters, CancellationToken, Task<PreviewSetDto>> previewGetting = async (parameters, token) =>
        {
            return await Mediator.Send(new GetNoveltySelectionQuery(parameters), cancellationToken);
        };

        var previewSet = await LookForComicsAsync(provider, previewGetting, cancellationToken);

        await ViewPreviewSetAsync(previewSet, provider, cancellationToken);
    }

    public async Task GetGoneOnSaleSelection(Update update, CancellationToken cancellationToken)
    {
        var provider = new SelectionMetadataProvider(update);

        Func<OptionParameters, CancellationToken, Task<PreviewSetDto>> previewGetting = async (parameters, token) =>
        {
            return await Mediator.Send(new GetGoneOnSaleSelectionQuery(parameters), cancellationToken);
        };

        var previewSet = await LookForComicsAsync(provider, previewGetting, cancellationToken);

        await ViewPreviewSetAsync(previewSet, provider, cancellationToken);
    }

    public async Task GetBackOnSaleSelection(Update update, CancellationToken cancellationToken)
    {
        var provider = new SelectionMetadataProvider(update);

        Func<OptionParameters, CancellationToken, Task<PreviewSetDto>> previewGetting = async (parameters, token) =>
        {
            return await Mediator.Send(new GetBackOnSaleSelectionQuery(parameters), cancellationToken);
        };

        var previewSet = await LookForComicsAsync(provider, previewGetting, cancellationToken);

        await ViewPreviewSetAsync(previewSet, provider, cancellationToken);
    }

    public async Task GetCurrentDayAuthorSelection(Update update, CancellationToken cancellationToken)
    {
        var provider = new SelectionMetadataProvider(update);

        Func<OptionParameters, CancellationToken, Task<PreviewSetDto>> previewGetting = async (parameters, token) =>
        {
            return await Mediator.Send(new GetCurrentDayAuthorSelectionQuery(parameters), cancellationToken);
        };

        var previewSet = await LookForComicsAsync(provider, previewGetting, cancellationToken);

        await ViewPreviewSetAsync(previewSet, provider, cancellationToken);
    }

    public async Task GetPopularSelection(Update update, CancellationToken cancellationToken)
    {
        var provider = new SelectionMetadataProvider(update);

        Func<OptionParameters, CancellationToken, Task<PreviewSetDto>> previewGetting = async (parameters, token) =>
        {
            return await Mediator.Send(new GetPopularSelectionQuery(parameters), cancellationToken);
        };

        var previewSet = await LookForComicsAsync(provider, previewGetting, cancellationToken);

        await ViewPreviewSetAsync(previewSet, provider, cancellationToken);
    }

    private async Task<PreviewSetDto> LookForComicsAsync(SelectionMetadataProvider provider, Func<OptionParameters, CancellationToken, 
        Task<PreviewSetDto>> previewGetting, CancellationToken cancellationToken)
    {
        await BotClient.SendTextMessageAsync(
            chatId: provider.GetChatId(),
            text: "Ищем комиксы по данной подборке...",
            cancellationToken: cancellationToken);

        var previewSetDto = await previewGetting(provider.BuildPagging(), cancellationToken);

        return previewSetDto;
    }
    private async Task ViewPreviewSetAsync(PreviewSetDto previewSet, SelectionMetadataProvider provider,
        CancellationToken cancellationToken)
    {
        if (previewSet.TotalCount == 0)
        {
            await BotClient.SendTextMessageAsync(
                chatId: provider.GetChatId(),
                text: "К сожалению в данный момент нет комиксов по данной подборке.",
                cancellationToken: cancellationToken);

            return;
        }

        var telegramBookPreviews = previewSet.Previews
            .Select(preview => Mapper.Map<TelegramPreviewDto>(preview))
            .ToList();

        foreach (var preview in telegramBookPreviews)
        {
            await BotClient.SendPhotoAsync(
                chatId: provider.GetChatId(),
                photo: preview.FileUrl,
                caption: provider.GetPreviewHtml(preview),
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
        }

        await BotClient.SendTextMessageAsync(
            chatId: provider.GetChatId(),
            text: "Ещё?",
            replyMarkup: provider.BuildNavigationButtons(previewSet.TotalCount),
            cancellationToken: cancellationToken);
    }
}

