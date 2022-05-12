using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Books;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.Queries;
using BookStore.Application.Notifications.BookCreated;
using BookStore.Application.Commands.BookEditing.UpdateBook;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.BookEditing.CreateBook;
using System.Threading;
using BookStore.Application.Commands.BookEditing.GetEditBook;
using BookStore.Application.Commands.BookEditing.DeleteBook;

namespace BookStore.WebApi.Areas.Dashboard.Controllers
{
    [Route("[area]/form-entity/book")]
    public class BookEditingController : ProductCrudController<Book, BookStore.WebApi.Areas.Dashboard.ViewModels.Forms.BookForm>
    {
        public BookEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Book> queryBuilder) : base(mediator, mapper, queryBuilder)
        {
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
}
