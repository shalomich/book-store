
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookStore.Domain.Entities;

using AutoMapper.QueryableExtensions;
using BookStore.Domain.Entities.Products;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.Application.Queries;
using BookStore.WebApi.Areas.Store.ViewModels.Cards;
using BookStore.WebApi.Areas.Store.ViewModels;
using BookStore.WebApi.Extensions;
using BookStore.Application.Dto;

namespace BookStore.WebApi.Areas.Store.Controllers
{
    public abstract class ProductCardController<T> : StoreController where T : Product
    {
        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }
        protected DbFormEntityQueryBuilder<T> ProductQueryBuilder { get; }

        protected ProductCardController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<T> productQueryBuilder)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ProductQueryBuilder = productQueryBuilder ?? throw new ArgumentNullException(nameof(productQueryBuilder));
        }

        protected abstract void IncludeRelatedEntities();

        [HttpGet]
        public async Task<ActionResult<ProductPreview[]>> GetPreviews([FromQuery] FilterArgs[] filters, [FromQuery] SortingArgs[] sortings, 
            [FromQuery] SearchArgs search, [FromQuery] PaggingArgs pagging)
        {
            ProductQueryBuilder
                .AddFilters(filters)
                .AddSortings(sortings)
                .AddSearch(search)
                .AddPagging(pagging)
                .AddIncludeRequirements(new ProductAlbumIncludeRequirement<T>());
            IncludeRelatedEntities();

            var products = await Mediator.Send(new GetQuery(ProductQueryBuilder));

            var productType = typeof(T);
            var productPreviewType = Mapper.GetDestinationType(productType, typeof(ProductPreview));

            var previews = products
                .Select(product => (ProductPreview) Mapper.Map(product, productType, productPreviewType))
                .ToArray();

            return previews;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCard>> GetCardById(int id)
        {
            var productType = typeof(T);
            var productCardType = Mapper.GetDestinationType(productType, typeof(ProductCard));

            ProductQueryBuilder.AddIncludeRequirements(new ProductAlbumIncludeRequirement<T>());
            IncludeRelatedEntities();

            var product = await Mediator.Send(new GetByIdQuery(id, ProductQueryBuilder));

            return (ProductCard) Mapper.Map(product, productType, productCardType);
        }

        [HttpHead]
        public async Task GetPaggingMetadata([FromQuery] FilterArgs[] filterArgs, [FromQuery] SearchArgs searchArgs, [FromQuery] PaggingArgs paggingArgs)
        {
            ProductQueryBuilder
                .AddFilters(filterArgs)
                .AddSearch(searchArgs);

            var metadata = await Mediator.Send(new GetMetadataQuery(paggingArgs, ProductQueryBuilder));
            
            HttpContext.Response.Headers.Add(metadata);
        }

        protected async Task<IEnumerable<RelatedEntityDto>> GetRelatedEntities<TRelatedEntity>(IDbQueryBuilder<TRelatedEntity> relatedEntityqueryBuilder) where TRelatedEntity : RelatedEntity
        {
            var relatedEntities = await Mediator.Send(new GetQuery(relatedEntityqueryBuilder));

            return relatedEntities.Select(relatedEntity => Mapper.Map<RelatedEntityDto>(relatedEntity));
        }
    }
}
