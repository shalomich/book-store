﻿using App.Areas.Dashboard.ViewModels;
using App.Entities;
using App.Requirements;
using App.Services.QueryBuilders;
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


namespace App.Areas.Dashboard.Controllers
{
    public abstract class ProductCrudController<TProduct, TForm> : FormEntityCrudController<TProduct, TForm>
        where TProduct : Product where TForm : ProductForm
    {
        protected ProductCrudController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<TProduct> queryBuilder) : base(mediator, mapper, queryBuilder)
        {
        }

        public override async Task<ActionResult<TForm[]>> Read([FromQuery] QueryTransformArgs args)
        {
            QueryBuilder.AddDataTransformation(args)
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