﻿
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
using BookStore.Domain.Entities;
using BookStore.Application.Commands.Editing.UpdateDiscount;
using BookStore.Application.Notifications.DiscountUpdated;
using System.Threading;
using BookStore.Application.Commands;

namespace BookStore.WebApi.Areas.Dashboard.Controllers
{
    public abstract class ProductCrudController<TProduct, TForm> : FormEntityCrudController<TProduct, TForm>
        where TProduct : Product where TForm : ProductForm
    {
        protected ProductCrudController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<TProduct> queryBuilder) : base(mediator, mapper, queryBuilder)
        {
        }

        public override async Task<ActionResult<TForm[]>> Read([FromQuery] PaggingArgs paggingArgs)
        {
            QueryBuilder
                .AddPagging(paggingArgs)
                .AddIncludeRequirements(new ProductAlbumIncludeRequirement<TProduct>());

            var products = await Mediator.Send(new GetQuery(QueryBuilder));
            
            return products
                .Select(product => Mapper.Map<TForm>(product))
                .ToArray();
        }

        protected override async Task<TProduct> ReadById(int id)
        {
            QueryBuilder.AddIncludeRequirements(new ProductAlbumIncludeRequirement<TProduct>());

            IncludeRelatedEntities(QueryBuilder);

            return (TProduct) await Mediator.Send(new GetByIdQuery(id, QueryBuilder));
        }

        public override async Task<int> Create(TForm productForm)
        {
            var product = Mapper.Map<TProduct>(productForm);

            return await Mediator.Send(new CreateProductCommand(product));
        }

        [HttpPut("{id}/discount")]
        public async Task<IActionResult> UpdateDiscount(int id, DiscountForm discountForm, CancellationToken cancellationToken)
        {
            await Mediator.Send(new SetDiscountCommand(id, discountForm), cancellationToken);

            await Mediator.Publish(new DiscountUpdatedNotification(id), cancellationToken);

            return NoContent();
        }

        protected abstract void IncludeRelatedEntities(DbFormEntityQueryBuilder<TProduct> queryBuilder);
    }
}
