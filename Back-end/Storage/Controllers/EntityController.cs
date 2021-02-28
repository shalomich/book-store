using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public abstract class EntityController<T> : Controller where T : Entity, new()
    {
        protected readonly Database _database;
        public EntityController(Database database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Read()
        {
            return await _database.Set<T>().AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Read(int id)
        {
            var entity = await _database.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
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
    }
}
