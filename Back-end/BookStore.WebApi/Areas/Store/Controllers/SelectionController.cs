using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Commands.Selection.GetNoveltySelection;
using System.Threading;
using BookStore.Application.Commands.Selection.GetGoneOnSaleSelection;
using BookStore.Application.Commands.Selection.GetBackOnSaleSelection;
using BookStore.Application.Commands.Selection.GetCanBeInterestingSelection;
using BookStore.Application.Commands.Selection.GetBattleSelection;
using QueryWorker.Args;
using BookStore.Application.Commands.Selection.GetSearchSelection;
using System.ComponentModel.DataAnnotations;
using BookStore.Application.Commands.Selection.GetSearchHints;
using BookStore.Application.Commands.Selection.GetLastViewedSelection;
using BookStore.Application.Commands.Selection.GetCurrentDayAuthorSelection;
using BookStore.Application.Providers;

namespace BookStore.WebApi.Areas.Store.Controllers;

[Route("[area]/selection/")]
public class SelectionController : StoreController
{
    private IMediator Mediator { get; }

    public SelectionController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet("search")]
    public async Task<PreviewSetDto> GetSearchSelection([FromQuery] SearchArgs search, [FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetSearchSelectionQuery(search, optionParameters), cancellationToken);
    }

    [HttpGet("search/hint")]
    public Task<SearchHintsDto> GetSearchHints([FromQuery][Required] SearchArgs search,
            [FromQuery] PaggingArgs pagging)
    {
        return Mediator.Send(new GetSearchHintsQuery(search, pagging));
    }

    [HttpGet(SelectionNames.Novelties)]
    public async Task<PreviewSetDto> GetNoveltySelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetNoveltySelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet(SelectionNames.GoneOnSale)]
    public async Task<PreviewSetDto> GetGoneOnSaleSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetGoneOnSaleSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet(SelectionNames.BackOnSale)]
    public async Task<PreviewSetDto> GetBackOnSaleSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBackOnSaleSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet(SelectionNames.CurrentDayAuthor)]
    public async Task<PreviewSetDto> GetCurrentDayAuthorSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetCurrentDayAuthorSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet(SelectionNames.Popular)]
    public async Task<PreviewSetDto> GetPopularSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetPopularSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet(SelectionNames.LastViewed)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<PreviewSetDto> GetLastViewedSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetLastViewedSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet(SelectionNames.CanBeInteresting)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<PreviewSetDto> GetCanBeInterestingSelection([FromQuery] int? tagCount,
        [FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetCanBeInterestingSelectionQuery(tagCount, optionParameters), cancellationToken);
    }

    [HttpGet(SelectionNames.SpecialForYou)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<PreviewSetDto> GetSpecialForYouSelection(
        [FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetSpecialForYouSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet("battle")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public async Task<PreviewSetDto> GetBattleSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBattleSelectionQuery(optionParameters), cancellationToken);
    }
}

