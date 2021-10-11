
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

namespace BookStore.WebApi.Areas.Store.Controllers
{
    public abstract class ProductCardController<T> : StoreController where T : Product
    {
        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }
        protected DbFormEntityQueryBuilder<T> QueryBuilder { get; }

        protected ProductCardController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<T> queryBuilder)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            QueryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        [HttpGet]
        public async Task<ActionResult<ProductCard[]>> GetCards([FromQuery] QueryTransformArgs args)
        {
            QueryBuilder.AddDataTransformation(args)
                .AddIncludeRequirements(new ProductAlbumIncludeRequirement<T>());

            var products = await Mediator.Send(new GetQuery(QueryBuilder));

            return products
                .Select(product => Mapper.Map<ProductCard>(product))
                .ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetCardById(int id)
        {
            var productType = typeof(T);
            var productCardType = Mapper.GetDestinationType(productType, typeof(ProductCard));

            QueryBuilder.AddIncludeRequirements(new ProductAlbumIncludeRequirement<T>());
            IncludeRelatedEntities(QueryBuilder);

            var entity = await Mediator.Send(new GetByIdQuery(id,QueryBuilder));

            return Ok(Mapper.Map(entity, productType, productCardType));
        }

        protected abstract void IncludeRelatedEntities(DbFormEntityQueryBuilder<T> queryBuilder); 

        [HttpHead]
        public async Task GetPaggingMetadata([FromQuery] QueryTransformArgs args)
        {
            int dataCount = QueryBuilder.Build().Count();

            QueryBuilder.AddDataTransformation(args);

            var metadata = await Mediator.Send(new GetMetadataQuery(dataCount, args.Pagging, QueryBuilder));
            
            HttpContext.Response.Headers.Add(metadata);
        }

        protected async Task<IEnumerable<Option>> GetRelatedEntityOptions<TRelatedEntity>(IDbQueryBuilder<TRelatedEntity> relatedEntityqueryBuilder) where TRelatedEntity : RelatedEntity
        {
            var relatedEntities = await Mediator.Send(new GetQuery(relatedEntityqueryBuilder));

            return relatedEntities
                .Select(relatedEntity => Mapper.Map<Option>(relatedEntity))
                .ToArray();
        }
    }
}
