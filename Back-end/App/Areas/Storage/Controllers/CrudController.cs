using App.Areas.Storage.Attributes.GenericController;
using App.Areas.Storage.Services;
using App.Areas.Storage.ViewModels;
using App.Entities;
using App.Extensions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using QueryWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Storage.RequestHandlers.CreateHandler;
using static App.Areas.Storage.RequestHandlers.DeleteHandler;
using static App.Areas.Storage.RequestHandlers.GetByIdHandler;
using static App.Areas.Storage.RequestHandlers.GetHandler;
using static App.Areas.Storage.RequestHandlers.UpdateHandler;
using static App.Areas.Storage.Services.FormGenerator;

namespace App.Areas.Storage.Controllers
{
    [ApiController]
    [Area("storage")]
    [Route("[area]/form/[controller]")]
    [GenericController()]
    public class CrudController<T> : Controller where T : EntityForm
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CrudController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private Type EntityType => _mapper.GetDestinationType(typeof(T));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Read([FromQuery] QueryArgs queryParams)
        {
            var entities = await _mediator.Send(new GetQuery(EntityType, queryParams));
           
            return Ok(entities.Select(entity => _mapper.Map<T>(entity)));
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
