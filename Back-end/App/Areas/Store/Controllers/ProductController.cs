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
using static App.Areas.Common.RequestHandlers.GetFormEntitiesHandler;
using static App.Areas.Common.RequestHandlers.GetEntityByIdHandler;
using App.Areas.Common.ViewModels;
using App.Areas.Store.ViewModels.Cards;
using App.Entities;
using App.Areas.Store.ViewModels;
using static App.Areas.Common.RequestHandlers.GetEntitiesHandler;

namespace App.Areas.Store.Controllers
{
    [ApiController]
    [Area("store")]
    [Route("[area]/product/[controller]")]
    public abstract class ProductController<T> : Controller where T : Product
    {
        protected IMediator Mediator;
        protected IMapper Mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<ValidQueryData<IEnumerable<ProductCard>>>> GetCards([FromQuery] QueryArgs queryParams)
        {
            var validQueryProducts = await Mediator.Send(new GetFormEntitiesQuery(typeof(T), queryParams));

            var productCards = validQueryProducts.Data
                .Select(entity => Mapper.Map<ProductCard>(entity));

            return Ok(new ValidQueryData<IEnumerable<ProductCard>>(productCards, validQueryProducts.Errors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetCardById(int id)
        {
            var productType = typeof(T);
            var productCardType = Mapper.GetDestinationType(productType, typeof(ProductCard));
            var entity = await Mediator.Send(new GetByIdQuery(id, productType));

            return Ok(Mapper.Map(entity, productType, productCardType));
        }

        protected async Task<IEnumerable<Option>> GetRelatedEntityOptions(Type relatedEntityType, PaggingArgs args)
        {
            var relatedEntities = await Mediator.Send(new GetEntitiesQuery(relatedEntityType, args));

            return relatedEntities.Select(genre => Mapper.Map<Option>(genre));
        }
    }
}
