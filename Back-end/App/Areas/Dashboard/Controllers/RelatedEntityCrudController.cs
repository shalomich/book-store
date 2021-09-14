using App.Areas.Dashboard.ViewModels.Forms;
using App.Attributes.GenericController;
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
    [Route("[area]/form-entity/[controller]")]
    [GenericController()]
    public class RelatedEntityCrudController<T> : FormEntityCrudController<T, RelatedEntityForm> where T : RelatedEntity
    {
        public RelatedEntityCrudController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public override async Task<ActionResult<RelatedEntityForm[]>> Read([FromQuery] QueryTransformArgs args)
        {
            var relatedEntities = (IQueryable<RelatedEntity>)await Mediator.Send(new GetQuery(FormEntityType));
            var transformedRelatedEntities = await Mediator.Send(new TransformQuery(relatedEntities, args));

            return transformedRelatedEntities
                .ProjectTo<RelatedEntityForm>(Mapper.ConfigurationProvider)
                .ToArray();
        }

        public override async Task<ActionResult<RelatedEntityForm>> Read(int id)
        {
            var relatedEntity = (RelatedEntity)await Mediator.Send(new GetByIdQuery(id, FormEntityType));
            return Ok(Mapper.Map<RelatedEntityForm>(relatedEntity));
        }
    }
}
