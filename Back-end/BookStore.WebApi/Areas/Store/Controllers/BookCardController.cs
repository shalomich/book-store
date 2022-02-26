
using BookStore.Domain.Entities.Books;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.WebApi.Areas.Store.ViewModels;
using BookStore.Domain.Entities;
using BookStore.Application.Dto;
using System.ComponentModel.DataAnnotations;
using BookStore.Application.Queries;
using BookStore.WebApi.Attributes;

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/product/book")]
    public class BookCardController : StoreController
    {
        private IMediator Mediator { get; }
        private IMapper Mapper { get; }
        public BookCardController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        [HttpGet]
        [TypeFilter(typeof(OptionalAuthorizeFilter))]
        public async Task<ActionResult<PreviewSetDto>> Search([FromQuery] SearchParameters searchParameters)
        {
            return await Mediator.Send(new SearchQuery(searchParameters));
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(OptionalAuthorizeFilter))]
        public async Task<ActionResult<CardDto>> GetCardById(int id)
        {
            return await Mediator.Send(new GetCardByIdQuery(id));
        }

        protected async Task<IEnumerable<RelatedEntityDto>> GetRelatedEntities<TRelatedEntity>(IDbQueryBuilder<TRelatedEntity> relatedEntityqueryBuilder) where TRelatedEntity : RelatedEntity
        {
            var relatedEntities = await Mediator.Send(new GetQuery(relatedEntityqueryBuilder));

            return relatedEntities.Select(relatedEntity => Mapper.Map<RelatedEntityDto>(relatedEntity));
        }

        [HttpGet("genre")]
        public async Task<IEnumerable<RelatedEntityDto>> GetGenres([FromServices] DbFormEntityQueryBuilder<Genre> relatedEntityQueryBuilder)
        {
            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("type")]
        public async Task<IEnumerable<RelatedEntityDto>> GetTypes([FromServices] DbFormEntityQueryBuilder<BookType> relatedEntityQueryBuilder)
        {
            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("ageLimit")]
        public async Task<IEnumerable<RelatedEntityDto>> GetAgeLimits([FromServices] DbFormEntityQueryBuilder<AgeLimit> relatedEntityQueryBuilder)
        {
            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("coverArt")]
        public async Task<IEnumerable<RelatedEntityDto>> GetCoverArts([FromServices] DbFormEntityQueryBuilder<CoverArt> relatedEntityQueryBuilder)
        {
            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("author")]
        public async Task<IEnumerable<RelatedEntityDto>> GetAuthors([FromQuery][Required] SearchArgs search, [FromQuery] PaggingArgs pagging, 
            [FromServices] DbFormEntityQueryBuilder<Author> relatedEntityQueryBuilder)
        {
            relatedEntityQueryBuilder
                .AddSearch(search)
                .AddPagging(pagging);

            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("publisher")]
        public async Task<IEnumerable<RelatedEntityDto>> GetPublishers([FromQuery][Required] SearchArgs search, [FromQuery] PaggingArgs pagging,
            [FromServices] DbFormEntityQueryBuilder<Publisher> relatedEntityQueryBuilder)
        {
            relatedEntityQueryBuilder
                .AddSearch(search)
                .AddPagging(pagging);

            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("tag")]
        public async Task<IEnumerable<RelatedEntityDto>> GetTags([FromQuery][Required] SearchArgs search, [FromQuery] PaggingArgs pagging,
            [FromServices] DbFormEntityQueryBuilder<Tag> relatedEntityQueryBuilder)
        {
            relatedEntityQueryBuilder
                .AddSearch(search)
                .AddPagging(pagging);

            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("hint")]
        public Task<SearchHintsDto> GetSearchHints([FromQuery][Required] SearchArgs search, 
            [FromQuery] PaggingArgs pagging)
        {
            return Mediator.Send(new GetSearchHintsQuery(search, pagging));
        }
    }
}
