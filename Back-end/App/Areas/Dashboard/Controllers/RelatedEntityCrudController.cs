using App.Areas.Dashboard.ViewModels.Forms;
using App.Attributes.GenericController;
using BookStore.Domain.Entities;
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
