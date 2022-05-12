
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Products;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.Application.Queries;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;
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

        protected override Task<TProduct> ReadById(int id)
        {
            return null;
        }
    }
}
