using AutoMapper;
using BookStore.Application.Queries;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using BookStore.WebApi.Areas.Store.ViewModels.Cards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using BookStore.WebApi.Extensions;

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/selection/")]
    public class SelectionController : StoreController
    {
        private IMediator Mediator { get; }
        private IMapper Mapper { get; }
        private DbFormEntityQueryBuilder<Book> Builder { get; }

        public SelectionController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Book> builder)
        {
            Mediator = mediator;
            Mapper = mapper;
            Builder = builder;
        }


        [HttpGet("{selection}")]
        public async Task<IEnumerable<ProductPreview>> GetSelection(Selection selection, 
            [FromQuery] FilterArgs[] filters, [FromQuery] SortingArgs[] sortings, [FromQuery] PaggingArgs pagging)
        {
            await Mediator.Send(new ChooseSelectionQuery(selection, Builder));

            Builder
                .AddFilters(filters)
                .AddSortings(sortings)
                .AddPagging(pagging);

            var books = await Mediator.Send(new GetQuery(Builder));

            return books.Select(book => Mapper.Map<BookPreview>(book));
        }

        [HttpHead("{selection}")]
        public async Task GetSelectionMetadata([FromRoute] Selection selection,
            [FromQuery] FilterArgs[] filters, [FromQuery] SortingArgs[] sortings, [FromQuery] PaggingArgs pagging)
        {
            await Mediator.Send(new ChooseSelectionQuery(selection, Builder));

            Builder
                .AddFilters(filters)
                .AddSortings(sortings);
                
            var metadata = await Mediator.Send(new GetMetadataQuery(pagging, Builder));

            HttpContext.Response.Headers.Add(metadata);
        }
    }
}
