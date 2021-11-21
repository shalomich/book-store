using AutoMapper;
using BookStore.Application.Queries.Selections;
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

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/selection/book")]
    public class SelectionController : StoreController
    {
        private IMediator Mediator { get; }
        private IMapper Mapper { get; }
        private DbFormEntityQueryBuilder<Book> Builder { get; }

        public SelectionController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Book> builder)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        [HttpGet("novelty")]
        public Task<IEnumerable<ProductCard>> GetNovelty([FromQuery] PaggingArgs pagging) 
        {
            return GetSelection(new GetNoveltyQuery(Builder), pagging);
        }

        [HttpGet("gone-on-sale")]
        public Task<IEnumerable<ProductCard>> GetGoneOnSale([FromQuery] PaggingArgs pagging)
        {
            return GetSelection(new GetGoneOnSaleQuery(Builder), pagging);
        }

        [HttpGet("for-children")]
        public Task<IEnumerable<ProductCard>> GetForChildren([FromQuery] PaggingArgs pagging)
        {
            return GetSelection(new GetForChildrenQuery(Builder), pagging);
        }

        [HttpGet("back-on-sale")]
        public Task<IEnumerable<ProductCard>> GetBackOnSale([FromQuery] PaggingArgs pagging)
        {
            return GetSelection(new GetBackOnSaleQuery(Builder), pagging);
        }

        private async Task<IEnumerable<ProductCard>> GetSelection(ISelectionQuery selectionQuery, PaggingArgs pagging)
        {
            Builder.AddPagging(pagging);

            var selection = await Mediator.Send(selectionQuery);

            return selection
                .Select(book => Mapper.Map<ProductCard>(book))
                .ToArray();
        }
    }
}
