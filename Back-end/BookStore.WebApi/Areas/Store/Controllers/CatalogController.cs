
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Commands.Selection.GetCatalogSelection;
using System.Threading;
using BookStore.Application.Commands.Catalog.GetBookCard;
using BookStore.Application.Commands.Catalog.ViewBook;
using BookStore.Application.Commands.Catalog.GetBookFilters;

namespace BookStore.WebApi.Areas.Store.Controllers;

[Route("[area]/[controller]")]
public class CatalogController : StoreController
{
    private IMediator Mediator { get; }
    public CatalogController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PreviewSetDto>> GetCatalogSelection([FromQuery] OptionParameters optionParameters,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetCatalogSelectionQuery(optionParameters), cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookCardDto>> GetCardById(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetCardByIdQuery(id), cancellationToken);
    }

    [HttpPost("{id}/view")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<NoContentResult> ViewBook(int id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new ViewBookCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpGet("filter")]
    public async Task<BookFiltersDto> GetBookFilters(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBookFiltersQuery(), cancellationToken);
    }
}

