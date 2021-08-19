using App.Areas.Storage.Attributes.GenericController;
using App.Areas.Storage.ViewModels;
using App.Areas.Storage.ViewModels.Cards;
using App.Extensions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Storage.RequestHandlers.GetByIdHandler;
using static App.Areas.Storage.RequestHandlers.GetHandler;

namespace App.Areas.Storage.Controllers
{
    [ApiController]
    [Area("storage")]
    [Route("[area]/card/[controller]")]
    [GenericController()]
    public class CardController<T> : Controller where T : ProductCard
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CardController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private Type ProductType => _mapper.GetSourceType(typeof(T));

        [HttpGet]
        public async Task<ActionResult<ValidQueryData<IEnumerable<ProductCard>>>> Read([FromQuery] QueryArgs queryParams)
        {
            var validQueryProducts = await _mediator.Send(new GetQuery(ProductType, queryParams));

            var productCards = validQueryProducts.Data
                .Select(entity => _mapper.Map<ProductCard>(entity));

            return Ok(new ValidQueryData<IEnumerable<ProductCard>>(productCards, validQueryProducts.Errors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Read(int id)
        {
            var entity = await _mediator.Send(new GetByIdQuery(id, ProductType));
            return Ok(_mapper.Map<T>(entity));
        }
    }
}
