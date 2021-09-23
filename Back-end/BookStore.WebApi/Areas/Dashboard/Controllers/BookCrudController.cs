
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
                new BookGenresIncludeRequirement()
            });
        }
    }
}
