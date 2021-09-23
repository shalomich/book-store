
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

        public override async Task<ActionResult<RelatedEntityForm[]>> Read([FromQuery] QueryTransformArgs args)
        {
            QueryBuilder.AddDataTransformation(args);

            var relatedEntities = await Mediator.Send(new GetQuery(QueryBuilder));

            return relatedEntities
                .Select(relatedEntity => Mapper.Map<RelatedEntityForm>(relatedEntity))
                .ToArray();
        }

        public override async Task<ActionResult<RelatedEntityForm>> Read(int id)
        {
            var relatedEntity = (RelatedEntity)await Mediator.Send(new GetByIdQuery(id, QueryBuilder));
            return Ok(Mapper.Map<RelatedEntityForm>(relatedEntity));
        }
    }
}
