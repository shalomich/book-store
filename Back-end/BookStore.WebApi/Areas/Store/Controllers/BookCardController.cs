
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

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/product/book")]
    public class BookCardController : ProductCardController<Book>
    {
        public BookCardController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Book> productQueryBuilder) : base(mediator, mapper, productQueryBuilder)
        {
        }

        protected override void IncludeRelatedEntities()
        {
            ProductQueryBuilder.AddIncludeRequirements(new IIncludeRequirement<Book>[]
            {
                new BookGenresIncludeRequirement(),
                new BookAuthorIncludeRequirement(),
                new BookPublisherIncludeRequirement(),
                new BookDefinitionIncludeRequirement()
            }); 
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
        
    }
}
