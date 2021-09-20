using App.Areas.Store.ViewModels;
using App.Entities;
using App.Entities.Books;
using App.Requirements;
using App.Services.QueryBuilders;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.Controllers
{
    [Route("[area]/product/book")]
    public class BookCardController : ProductCardController<Book>
    {
        public BookCardController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Book> queryBuilder) : base(mediator, mapper, queryBuilder)
        {
        }

        protected override void IncludeRelatedEntities(DbFormEntityQueryBuilder<Book> queryBuilder)
        {
            queryBuilder.AddIncludeRequirements(new IIncludeRequirement<Book>[]
            {
                new BookGenresIncludeRequirement(),
                new BookAuthorIncludeRequirement(),
                new BookPublisherIncludeRequirement(),
                new BookDefinitionIncludeRequirement()
            }); 
        }

        [HttpGet("genre")]
        public async Task<IEnumerable<Option>> GetGenreOptions([FromServices] DbFormEntityQueryBuilder<Genre> queryBuilder)
        {
            return await GetRelatedEntityOptions(queryBuilder);
        }

        [HttpGet("type")]
        public async Task<IEnumerable<Option>> GetTypeOptions([FromServices] DbFormEntityQueryBuilder<BookType> queryBuilder)
        {
            return await GetRelatedEntityOptions(queryBuilder);
        }

        [HttpGet("age-limit")]
        public async Task<IEnumerable<Option>> GetAgeLimitOptions([FromServices] DbFormEntityQueryBuilder<AgeLimit> queryBuilder)
        {
            return await GetRelatedEntityOptions(queryBuilder);
        }

        [HttpGet("cover-art")]
        public async Task<IEnumerable<Option>> GetCoverArtOptions([FromServices] DbFormEntityQueryBuilder<CoverArt> queryBuilder)
        {
            return await GetRelatedEntityOptions(queryBuilder);
        }

        [HttpGet("author")]
        public async Task<IEnumerable<Option>> GetAuthorOptions([FromServices] DbFormEntityQueryBuilder<Author> queryBuilder)
        {
            return await GetRelatedEntityOptions(queryBuilder);
        }

        [HttpGet("publisher")]
        public async Task<IEnumerable<Option>> GetPublisherOptions([FromServices] DbFormEntityQueryBuilder<Publisher> queryBuilder)
        {
            return await GetRelatedEntityOptions(queryBuilder);
        }
        
    }
}
