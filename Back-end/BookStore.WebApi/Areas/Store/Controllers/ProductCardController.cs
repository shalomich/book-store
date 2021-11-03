
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
        public async Task<ActionResult<ProductCard[]>> GetCards([FromQuery] QueryTransformArgs transformArgs, [FromQuery] PaggingArgs paggingArgs)
        {
            ProductQueryBuilder
                .AddDataTransformation(transformArgs)
                .AddPagging(paggingArgs)
                .AddIncludeRequirements(new ProductAlbumIncludeRequirement<T>());

            var products = await Mediator.Send(new GetQuery(ProductQueryBuilder));

            return products
                .Select(product => Mapper.Map<ProductCard>(product))
                .ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetCardById(int id)
        {
            var productType = typeof(T);
            var productCardType = Mapper.GetDestinationType(productType, typeof(ProductCard));

            ProductQueryBuilder.AddIncludeRequirements(new ProductAlbumIncludeRequirement<T>());
            IncludeRelatedEntities();

            var entity = await Mediator.Send(new GetByIdQuery(id, ProductQueryBuilder));

            return Ok(Mapper.Map(entity, productType, productCardType));
        }

        [HttpHead]
        public async Task GetPaggingMetadata([FromQuery] QueryTransformArgs transformArgs, [FromQuery] PaggingArgs paggingArgs)
        {
            ProductQueryBuilder.AddDataTransformation(transformArgs);

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
