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
        public async Task<ActionResult<ProductCard[]>> GetNovelty([FromQuery] PaggingArgs pagging) 
        {
            Builder.AddPagging(pagging);

            return (await Mediator.Send(new GetNoveltyQuery(Builder)))
                .Select(book => Mapper.Map<ProductCard>(book))
                .ToArray();
        }

        [HttpGet("gone-on-sale")]
        public async Task<ActionResult<ProductCard[]>> GetGoneOnSale([FromQuery] PaggingArgs pagging)
        {
            Builder.AddPagging(pagging);

            return (await Mediator.Send(new GetGoneOnSaleQuery(Builder)))
                .Select(book => Mapper.Map<ProductCard>(book))
                .ToArray();
        }

        [HttpGet("for-children")]
        public async Task<ActionResult<ProductCard[]>> GetForChildren([FromQuery] PaggingArgs pagging)
        {
            Builder.AddPagging(pagging);

            return (await Mediator.Send(new GetForChildrenQuery(Builder)))
                .Select(book => Mapper.Map<ProductCard>(book))
                .ToArray();
        }

        [HttpGet("back-on-sale")]
        public async Task<ActionResult<ProductCard[]>> GetBackOnSale([FromQuery] PaggingArgs pagging)
        {
            Builder.AddPagging(pagging);

            return (await Mediator.Send(new GetBackOnSaleQuery(Builder)))
                .Select(book => Mapper.Map<ProductCard>(book))
                .ToArray();
        }
    }
}
