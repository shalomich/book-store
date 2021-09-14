using App.Areas.Dashboard.ViewModels;
using App.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetByIdHandler;
using static App.Areas.Common.RequestHandlers.GetHandler;
using static App.Areas.Common.RequestHandlers.TransformHandler;

namespace App.Areas.Dashboard.Controllers
{
    public abstract class ProductCrudController<TProduct, TForm> : FormEntityCrudController<TProduct, TForm>
        where TProduct : Product where TForm : ProductForm
    {
        protected ProductCrudController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        public override async Task<ActionResult<TForm[]>> Read([FromQuery] QueryTransformArgs args)
        {
            var products = (IQueryable<Product>)await Mediator.Send(new GetQuery(FormEntityType));
            var transformedProducts = await Mediator.Send(new TransformQuery(products, args));

            return transformedProducts
                .ProjectTo<TForm>(Mapper.ConfigurationProvider)
                .ToArray();
        }

        public override async Task<ActionResult<TForm>> Read(int id)
        {
            var product = (Product)await Mediator.Send(new GetByIdQuery(id, FormEntityType));
            return Ok(Mapper.Map<TForm>(product));
        }
    }
}
