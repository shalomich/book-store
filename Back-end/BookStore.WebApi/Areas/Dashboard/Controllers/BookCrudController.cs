
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Books;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;
using BookStore.Application.Queries;
using BookStore.Application.Notifications.BookCreated;
using BookStore.Application.Notifications.BookUpdated;

namespace BookStore.WebApi.Areas.Dashboard.Controllers
{
    [Route("[area]/form-entity/book")]
    public class BookCrudController : ProductCrudController<Book, BookForm>
    {
        public BookCrudController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Book> queryBuilder) : base(mediator, mapper, queryBuilder)
        {
        }

        protected override void IncludeRelatedEntities(DbFormEntityQueryBuilder<Book> queryBuilder)
        {
            queryBuilder.AddIncludeRequirements(new IIncludeRequirement<Book>[]
            {
                new BookGenresIncludeRequirement(),
                new BookTagsIncludeRequirement()
            });
        }

        public override async Task<int> Create(BookForm entityForm)
        {
            int createdBookId = await base.Create(entityForm);
            
            await Mediator.Publish(new BookCreatedNotification(createdBookId));

            return createdBookId;
        }

        public override async Task<IActionResult> Update(int id, BookForm entityForm, [FromServices] DbFormEntityQueryBuilder<Book> queryBuilder)
        {
            var result = await base.Update(id, entityForm, queryBuilder);

            await Mediator.Publish(new BookUpdatedNotification(id));

            return result;
        }

        [HttpGet("isbn-existed")]
        public async Task<bool> CheckIsbnExisted([FromQuery] string isbn)
        {
            return await Mediator.Send(new CheckIsbnExistedQuery(isbn));
        }
    }
}
