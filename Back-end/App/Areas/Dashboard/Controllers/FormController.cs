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
using static App.Areas.Common.RequestHandlers.GetByQueryHandler;
using static App.Areas.Common.RequestHandlers.GetByIdHandler;
using static App.Areas.Common.RequestHandlers.CreateHandler;
using static App.Areas.Common.RequestHandlers.UpdateHandler;
using static App.Areas.Common.RequestHandlers.DeleteHandler;
using App.Areas.Dashboard.Services;
using static App.Areas.Dashboard.Services.FormGenerator;
using App.Areas.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace App.Areas.Dashboard.Controllers
{
    [ApiController]
    [Area("dashboard")]
    [Route("[area]/form/[controller]")]
    [GenericController()]
    public class FormController<T> : Controller where T : EntityForm
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FormController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private Type EntityType => _mapper.GetSourceType(typeof(T));

        [HttpGet]

        public async Task<ActionResult<ValidQueryData<IEnumerable<EntityIdentity>>>> Read([FromQuery] QueryArgs queryParams)
        {
            var validQueryEntities = await _mediator.Send(new GetByQuery(EntityType, queryParams));

            var entityIdentities = validQueryEntities.Data
                .Select(entity => _mapper.Map<EntityIdentity>(entity));

            return Ok(new ValidQueryData<IEnumerable<EntityIdentity>>(entityIdentities, validQueryEntities.Errors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Read(int id)
        {
            var entity = await _mediator.Send(new GetByIdQuery(id, EntityType));
            return Ok(_mapper.Map<T>(entity));
        }

        [HttpPost] 
        public async Task<IActionResult> Create(T entityForm) 
        {
            var entity = _mapper.Map(entityForm, typeof(T), EntityType) as Entity;
            var createdEntity = await _mediator.Send(new CreateCommand(entity));

            return CreatedAtAction(nameof(Read), new { id = createdEntity.Id }, createdEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, T entityForm)
        {
            var entity = _mapper.Map(entityForm, typeof(T), EntityType) as Entity;

            await _mediator.Send(new UpdateCommand(id,entity));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedEntity = await _mediator.Send(new GetByIdQuery(id, EntityType));
            await _mediator.Send(new DeleteCommand(deletedEntity));

            return NoContent();
        }

        [HttpGet("template")]
        public IEnumerable<FormField> GetFormTemplate([FromServices] FormGenerator formConverter)
        {
            return formConverter.Convert(typeof(T));
        }
    }
}
