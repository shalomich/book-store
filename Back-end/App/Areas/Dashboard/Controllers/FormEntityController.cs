using App.Attributes.GenericController;
using App.Areas.Store.Services;
using App.Areas.Dashboard.ViewModels;
using App.Areas.Dashboard.ViewModels.Identities;
using App.Entities;
using App.Extensions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetByIdHandler;
using static App.Areas.Common.RequestHandlers.CreateHandler;
using static App.Areas.Common.RequestHandlers.UpdateHandler;
using static App.Areas.Common.RequestHandlers.DeleteHandler;
using App.Areas.Dashboard.Services;
using static App.Areas.Dashboard.Services.FormGenerator;
using App.Areas.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using static App.Areas.Common.RequestHandlers.GetHandler;
using static App.Areas.Common.RequestHandlers.TransformHandler;
using AutoMapper.QueryableExtensions;

namespace App.Areas.Dashboard.Controllers
{
    [ApiController]
    [Area("dashboard")]
    [Route("[area]/form-entity/[controller]")]
    [GenericController()]
    public class FormEntityController<T> : Controller where T : EntityForm
    {
        private readonly IMediator Mediator;
        private readonly IMapper Mapper;

        public FormEntityController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private Type FormEntityType => Mapper.GetSourceType(typeof(T));

        [HttpGet]

        public async Task<ActionResult<FormEntityIdentitiesByQuery>> Read([FromQuery] QueryTransformArgs args)
        {
            var formEntities = (IQueryable<FormEntity>) await Mediator.Send(new GetQuery(FormEntityType));
            FormEntitiesByQuery formEntitiesByQuery = await Mediator.Send(new TransformQuery(formEntities, args));
            var formEntityIdentities = formEntitiesByQuery.FormEntities
                .ProjectTo<FormEntityIdentity>(Mapper.ConfigurationProvider)
                .ToArray();

            return Mapper.Map<FormEntityIdentitiesByQuery>(formEntitiesByQuery) with { FormEntityIdentities = formEntityIdentities };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Read(int id)
        {
            var entity = (FormEntity) await Mediator.Send(new GetByIdQuery(id, FormEntityType));
            return Ok(Mapper.Map<T>(entity));
        }

        [HttpPost] 
        public async Task<IActionResult> Create(T entityForm) 
        {
            var entity = (FormEntity) Mapper.Map(entityForm, typeof(T), FormEntityType);
            var createdEntity = await Mediator.Send(new CreateCommand(entity));

            return CreatedAtAction(nameof(Read), new { id = createdEntity.Id }, Mapper.Map<T>(createdEntity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, T entityForm)
        {
            var entity = (FormEntity) Mapper.Map(entityForm, typeof(T), FormEntityType);

            await Mediator.Send(new UpdateCommand(id,entity));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedEntity = (FormEntity) await Mediator.Send(new GetByIdQuery(id, FormEntityType));
            await Mediator.Send(new DeleteCommand(deletedEntity));

            return NoContent();
        }

        [HttpGet("form-template")]
        public IEnumerable<FormField> GetFormTemplate([FromServices] FormGenerator formConverter)
        {
            return formConverter.Convert(typeof(T));
        }
    }
}
