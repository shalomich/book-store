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

namespace App.Areas.Storage.Controllers
{
    [ApiController]
    [Route("storage/[controller]")]
    public abstract class EntityController<T> : Controller where T : Entity,new()
    {
        
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
        public ActionResult<T> Create(T entity) 
        {
            return null;
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
