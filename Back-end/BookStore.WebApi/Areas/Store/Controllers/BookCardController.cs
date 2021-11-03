
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
        public async Task<IEnumerable<IEntity>> GetGenres([FromServices] DbFormEntityQueryBuilder<Genre> relatedEntityQueryBuilder)
        {
  
            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("type")]
        public async Task<IEnumerable<IEntity>> GetTypes([FromServices] DbFormEntityQueryBuilder<BookType> relatedEntityQueryBuilder)
        {
            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("age-limit")]
        public async Task<IEnumerable<IEntity>> GetAgeLimits([FromServices] DbFormEntityQueryBuilder<AgeLimit> relatedEntityQueryBuilder)
        {
            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("cover-art")]
        public async Task<IEnumerable<IEntity>> GetCoverArts([FromServices] DbFormEntityQueryBuilder<CoverArt> relatedEntityQueryBuilder)
        {
            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("author")]
        public async Task<IEnumerable<IEntity>> GetAuthors([FromRoute] PaggingArgs paggingArgs, 
            [FromServices] DbFormEntityQueryBuilder<Author> relatedEntityQueryBuilder)
        {
            relatedEntityQueryBuilder.AddPagging(paggingArgs);

            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }

        [HttpGet("publisher")]
        public async Task<IEnumerable<IEntity>> GetPublishers([FromRoute] PaggingArgs paggingArgs, 
            [FromServices] DbFormEntityQueryBuilder<Publisher> relatedEntityQueryBuilder)
        {
            relatedEntityQueryBuilder.AddPagging(paggingArgs);

            return await GetRelatedEntities(relatedEntityQueryBuilder);
        }
        
    }
}
