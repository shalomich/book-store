using BookStore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Application.Dto;
using BookStore.Application.Services.CatalogSelections;
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

    [HttpGet("novelty")]
    public async Task<PreviewSetDto> GetNoveltySelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetNoveltySelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet("goneOnSale")]
    public async Task<PreviewSetDto> GetGoneOnSaleSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetGoneOnSaleSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet("backOnSale")]
    public async Task<PreviewSetDto> GetBackOnSaleSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBackOnSaleSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet("canBeInteresting")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<PreviewSetDto> GetCanBeInterestingSelection([FromQuery] int? tagCount,
        [FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetCanBeInterestingSelectionQuery(tagCount, optionParameters), cancellationToken);
    }

    [HttpGet("battle")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<PreviewSetDto> GetBattleSelection([FromQuery] OptionParameters optionParameters, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBattleSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet("lastViewed")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<PreviewSetDto> FindLastViewed([FromQuery] OptionParameters optionParameters, [FromServices] LastViewedSelection lastViewedSelection)
    {
        return null;
        //return await Mediator.Send(new GetCatalogSelectionQuery(lastViewedSelection, optionParameters));
    }
}

