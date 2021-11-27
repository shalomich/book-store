using AutoMapper;
using BookStore.Application.DbQueryConfigs.SelectionConfigs;
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

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/selection/book")]
    public class SelectionController : StoreController
    {
        private IMediator Mediator { get; }
        private IMapper Mapper { get; }
        public SelectionController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("novelty")]
        public Task<IEnumerable<ProductPreview>> GetNovelty([FromQuery] PaggingArgs pagging, [FromQuery] SortingArgs[] sortings, [FromQuery] FilterArgs[] filters) 
        {
            return GetSelection(new GetSelectionQuery(new NoveltyConfig(), pagging, filters, sortings));
        }

        [HttpGet("gone-on-sale")]
        public Task<IEnumerable<ProductPreview>> GetGoneOnSale([FromQuery] PaggingArgs pagging, [FromQuery] SortingArgs[] sortings, [FromQuery] FilterArgs[] filters)
        {
            return GetSelection(new GetSelectionQuery(new GoneOnSaleConfig(), pagging, filters, sortings));
        }

        [HttpGet("for-children")]
        public Task<IEnumerable<ProductPreview>> GetForChildren([FromQuery] PaggingArgs pagging, [FromQuery] SortingArgs[] sortings, [FromQuery] FilterArgs[] filters)
        {
            return GetSelection(new GetSelectionQuery(new ForChildrenConfig(), pagging, filters, sortings));
        }

        [HttpGet("back-on-sale")]
        public Task<IEnumerable<ProductPreview>> GetBackOnSale([FromQuery] PaggingArgs pagging, [FromQuery] SortingArgs[] sortings, [FromQuery] FilterArgs[] filters)
        {
            return GetSelection(new GetSelectionQuery(new BackOnSaleConfig(), pagging, filters, sortings));
        }

        [HttpGet("random-author")]
        public async Task<IEnumerable<ProductPreview>> GetByRandomAuthor([FromQuery] PaggingArgs pagging, [FromQuery] SortingArgs[] sortings, [FromQuery] FilterArgs[] filters)
        {
            int authorId = (await Mediator.Send(new GetSelectionAuthorQuery())).Id;

            return await GetSelection(new GetSelectionQuery(new ByAuthorConfig(authorId), pagging, filters, sortings));
        }
        private async Task<IEnumerable<ProductPreview>> GetSelection(GetSelectionQuery selectionQuery)
        {
            var selection = await Mediator.Send(selectionQuery);

            return selection
                .Select(book => Mapper.Map<ProductPreview>(book))
                .ToArray();
        }
    }
}
