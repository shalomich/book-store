using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using QueryWorker;
using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    [ApiController]
    [Route("storage/[controller]")]
    public abstract class EntityController<T> : Controller where T : Entity,new()
    {
        private IRepository<T> _repository;
        public EntityController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<T>> Read([FromQuery] QueryParams parameters, [FromServices] QueryTransformer<T> transformer)
        {
            var data = _repository.Select();
            data = transformer.Transform(data.AsQueryable(), parameters).ToList();

            foreach (var message in transformer.Informer.Messages)
                Response.Headers.Add(message.Key, message.Value);
            return new JsonResult(data);
        }

        [HttpGet("{id}")]
        public ActionResult<T> Read(int id)
        {
            return _repository.Select(id);
        }

        [HttpPost] 
        public ActionResult<T> Create(T entity) 
        { 
            if (entity == null) 
                return BadRequest();

            _repository.Create(entity);
            
            return CreatedAtAction(nameof(Read), new { id = entity.Id }, entity); 
        }

        [HttpPut("{id}")]
        public ActionResult<T> Update(int id, T entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            _repository.Update(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<T> Delete(int id)
        {
            try
            {
                _repository.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }            
            return NoContent();
        }

        [HttpGet("form")]
        public IActionResult ToForm([FromServices] EntityToFormConverter converter)
        {
            return new JsonResult(converter.Convert<T>());
        }

        [HttpGet("config")]
        public IActionResult ToConfig([FromServices] EntityConfig<T> config)
        {
            return new JsonResult(config.GetConstants());
        }

    }
}
