using App.Attributes.GenericController;
using App.Extensions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetByIdHandler;
using App.Areas.Common.ViewModels;
using App.Areas.Store.ViewModels.Cards;
using App.Entities;
using App.Areas.Store.ViewModels;
using static App.Areas.Common.RequestHandlers.GetHandler;
using static App.Areas.Common.RequestHandlers.TransformHandler;
using AutoMapper.QueryableExtensions;
using QueryWorker;
using static App.Areas.Common.RequestHandlers.GetQueryMetadataHandler;

namespace App.Areas.Store.Controllers
{
    public abstract class ProductCardController<T> : StoreController where T : Product
    {
        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }

        public ProductCardController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<ProductCard[]>> GetCards([FromQuery] QueryTransformArgs args)
        {
            var productType = typeof(T);
            var products = (IQueryable<Product>) await Mediator.Send(new GetQuery(productType));
            var transformedProducts = await Mediator.Send(new TransformQuery(products, args));
            return transformedProducts
                .ProjectTo<ProductCard>(Mapper.ConfigurationProvider)
                .ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetCardById(int id)
        {
            var productType = typeof(T);
            var productCardType = Mapper.GetDestinationType(productType, typeof(ProductCard));
            var entity = await Mediator.Send(new GetByIdQuery(id, productType));

            return Ok(Mapper.Map(entity, productType, productCardType));
        }

        [HttpGet("metadata")]
        public async Task<QueryMetadata> GetQueryMetadata([FromQuery] QueryTransformArgs args)
        {
            var productType = typeof(T);

            var products = (IQueryable<Product>)await Mediator.Send(new GetQuery(productType));

            return await Mediator.Send(new GetMetadataQuery(products, args));
        }

        protected async Task<IEnumerable<Option>> GetRelatedEntityOptions(Type relatedEntityType)
        {
            var relatedEntities = (IQueryable<RelatedEntity>) await Mediator.Send(new GetQuery(relatedEntityType));

            return relatedEntities.ProjectTo<Option>(Mapper.ConfigurationProvider).ToList();
        }
    }
}
