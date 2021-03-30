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
        protected readonly Database _database;

        protected abstract IQueryable<T> Data { get; }
        public EntityController(Database database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Read([FromQuery] QueryParams parameters, [FromServices] QueryTransformer<T> transformer)
        {
            var data = await Data.AsNoTracking().ToListAsync();
            return transformer.Transform(data.AsQueryable(), parameters).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Read(int id)
        {
            var entity = await Data.FirstOrDefaultAsync(entity => entity.Id == id);
            if (entity == null)
                return NotFound();
            else return entity;
        }

        [HttpPost] 
        public async Task<ActionResult<T>> Create(T entity) 
        { 
            if (entity == null) 
                return BadRequest();
            
            _database.Set<T>().Add(entity); 
            await _database.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Read), new { id = entity.Id }, entity); 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<T>> Update(int id, T entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            _database.Entry(entity).State = EntityState.Modified;

            await _database.SaveChangesAsync();
          
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<T>> Update(int id, [FromBody] JsonPatchDocument<T> patchEntity)
        {
            var entity = await _database.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            patchEntity.ApplyTo(entity, ModelState);
            await _database.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<T>> Delete(int id)
        {
            var entity = await _database.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            _database.Set<T>().Remove(entity);
            await _database.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpGet("form")]
        public IActionResult ToForm([FromServices] EntityToFormConverter converter)
        {
            return new JsonResult(converter.Convert<T>());
        }

        [HttpGet("config")]
        public IActionResult ToConfig([FromServices] IConfiguration configuration)
        {
            var entityName = typeof(T).Name;
            var entityConstantSection = configuration.GetSection($"entityConstants:{entityName}");
            
            Dictionary<string,object> options = entityConstantSection
                .GetSection("options")
                .GetChildren()
                .ToDictionary(section => section.Key, section => (object) section.Get<string[]>());
            Dictionary<string, object> numbers = entityConstantSection
                .GetSection("numbers")
                .GetChildren()
                .ToDictionary(section => section.Key, section => (object)section.Get<int>());
            Dictionary<string, object> strings = entityConstantSection
                .GetSection("strings")
                .GetChildren()
                .ToDictionary(section => section.Key, section => (object)section.Get<string>());


            return new JsonResult(options
                .Concat(numbers)
                .Concat(strings)
                .ToDictionary(property => property.Key, property => property.Value));

        }

    }
}
