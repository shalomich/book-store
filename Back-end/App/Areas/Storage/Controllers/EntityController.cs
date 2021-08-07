using App.Entities;
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

namespace App.Areas.Storage.Controllers
{
    [ApiController]
    [Area("storage")]
    [Route("[area]/[controller]")]
    public abstract class EntityController<T> : Controller where T : Entity,new()
    {
        private readonly IMediator _mediator;

        protected EntityController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public ActionResult<IEnumerable<T>> Read([FromQuery] QueryParams parameters, [FromServices] QueryTransformer<T> transformer)
        {
            return null;
        }

        [HttpGet("{id}")]
        public ActionResult<T> Read(int id)
        {
            return null;
        }

        [HttpPost] 
        public async Task<ActionResult<T>> Create(T entity) 
        {
            var createdEntity = await _mediator.Send(new CreateCommand(entity));

            return CreatedAtAction(nameof(Read), new { id = createdEntity.Id }, createdEntity);
        }

        [HttpPut("{id}")]
        public ActionResult<T> Update(int id, T entity)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public ActionResult<T> Delete(int id)
        {
            return null;
        }
    }
}
