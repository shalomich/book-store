
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookStore.Domain.Entities.Products;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.Application.Queries;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;

namespace BookStore.WebApi.Areas.Dashboard.Controllers
{
    public abstract class ProductCrudController<TProduct, TForm> : FormEntityCrudController<TProduct, TForm>
        where TProduct : Product where TForm : ProductForm
    {
        protected ProductCrudController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<TProduct> queryBuilder) : base(mediator, mapper, queryBuilder)
        {
        }

        public override async Task<ActionResult<TForm[]>> Read([FromQuery] QueryTransformArgs transformArgs, [FromQuery] PaggingArgs paggingArgs)
        {
            QueryBuilder
                .AddDataTransformation(transformArgs)
                .AddPagging(paggingArgs)
                .AddIncludeRequirements(new ProductAlbumIncludeRequirement<TProduct>());

            var products = await Mediator.Send(new GetQuery(QueryBuilder));
            
            return products
                .Select(product => Mapper.Map<TForm>(product))
                .ToArray();
        }

        public override async Task<ActionResult<TForm>> Read(int id)
        {
            QueryBuilder.AddIncludeRequirements(new ProductAlbumIncludeRequirement<TProduct>());

            IncludeRelatedEntities(QueryBuilder);

            var product = (Product)await Mediator.Send(new GetByIdQuery(id, QueryBuilder));
            
            return Ok(Mapper.Map<TForm>(product));
        }

        protected abstract void IncludeRelatedEntities(DbFormEntityQueryBuilder<TProduct> queryBuilder);
    }
}
