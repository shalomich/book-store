using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Books;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.Queries;
using BookStore.Application.Commands.BookEditing.UpdateBook;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.BookEditing.CreateBook;
using System.Threading;
using BookStore.Application.Commands.BookEditing.GetEditBook;
using BookStore.Application.Commands.BookEditing.DeleteBook;
using System.Linq;
using QueryWorker.Args;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/book")]
[ApiController]
[Area("dashboard")]
public class BookEditingController : Controller
{
    public IMediator Mediator { get; }
    public IMapper Mapper { get; }
    public DbFormEntityQueryBuilder<Book> QueryBuilder { get; }

    public BookEditingController(
        IMediator mediator, 
        IMapper mapper, 
        DbFormEntityQueryBuilder<Book> queryBuilder)
    {
        Mediator = mediator;
        Mapper = mapper;
        QueryBuilder = queryBuilder;
    }

    [HttpHead]
    public async Task GetPaggingMetadata([FromQuery] PaggingArgs paggingArgs)
    {
        var metadata = await Mediator.Send(new GetMetadataQuery(paggingArgs, QueryBuilder));

        HttpContext.Response.Headers.Add(metadata);
    }

    [HttpGet]
    public async Task<BookForm[]> GetBooks([FromQuery] PaggingArgs pagging, CancellationToken cancellationToken)
    {
        QueryBuilder
            .AddPagging(pagging)
            .AddIncludeRequirements(new ProductAlbumIncludeRequirement<Book>());

        var products = await Mediator.Send(new GetQuery(QueryBuilder));

        return products
            .Select(product => Mapper.Map<BookForm>(product))
            .ToArray();
    }

    [HttpGet("{id}")]
    public async Task<BookForm> GetEditBook(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetEditBookQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<int> CreateBook([FromBody] BookForm bookForm, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new CreateBookCommand(bookForm), cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookForm bookForm, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateBookCommand(id, bookForm), cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteBookCommand(id), cancellationToken);

        return NoContent();
    }


    [HttpGet("isbn-existed")]
    public async Task<bool> CheckIsbnExisted([FromQuery] string isbn)
    {
        return await Mediator.Send(new CheckIsbnExistedQuery(isbn));
    }
}

