
using BookStore.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.Queries;
using BookStore.WebApi.Attributes.GenericController;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;

namespace BookStore.WebApi.Areas.Dashboard.Controllers
{
    [Route("[area]/form-entity/[controller]")]
    [GenericController()]
    public class RelatedEntityCrudController<T> : FormEntityCrudController<T, RelatedEntityForm> where T : RelatedEntity
    {
        public RelatedEntityCrudController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<T> queryBuilder) : base(mediator, mapper, queryBuilder)
        {
        }

        public override async Task<ActionResult<RelatedEntityForm[]>> Read([FromQuery] QueryTransformArgs transformArgs, [FromQuery] PaggingArgs paggingArgs)
        {
            QueryBuilder
                .AddDataTransformation(transformArgs)
                .AddPagging(paggingArgs);

            var relatedEntities = await Mediator.Send(new GetQuery(QueryBuilder));

            return relatedEntities
                .Select(relatedEntity => Mapper.Map<RelatedEntityForm>(relatedEntity))
                .ToArray();
        }

        protected override async Task<T> ReadById(int id)
        {
            return (T) await Mediator.Send(new GetByIdQuery(id, QueryBuilder));
        }

        [HttpGet("name-existed")]
        public async Task<bool> CheckNameExisted([FromQuery] string name)
        {
            return await Mediator.Send(new CheckRelatedEntityNameQuery(name, QueryBuilder));
        }
    }
}
