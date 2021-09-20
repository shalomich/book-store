using App.Areas.Dashboard.ViewModels;
using App.Entities;
using App.Requirements;
using App.Services.QueryBuilders;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.Controllers
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
