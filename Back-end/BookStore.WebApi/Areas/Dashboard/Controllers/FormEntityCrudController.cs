
using BookStore.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.Queries;
using BookStore.Application.Exceptions;
using BookStore.Application.Commands;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;
using BookStore.WebApi.Extensions;
using BookStore.Domain.Entities.Books;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;

namespace BookStore.WebApi.Areas.Dashboard.Controllers
{
    [ApiController]
    [Area("dashboard")]
    public abstract class FormEntityCrudController<TFormEntity, TForm> : Controller where TFormEntity : class, IFormEntity
        where TForm : IEntityForm
    {
        protected IMediator Mediator { get; }
        protected IMapper Mapper { get;}
        protected DbFormEntityQueryBuilder<TFormEntity> QueryBuilder { get; }

        protected FormEntityCrudController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<TFormEntity> queryBuilder)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            QueryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        protected Type FormEntityType => typeof(TFormEntity);

        [HttpGet]
        public abstract Task<ActionResult<TForm[]>> Read([FromQuery] QueryTransformArgs args, [FromQuery] PaggingArgs paggingArgs);

        protected abstract Task<TFormEntity> ReadById(int id);

        [HttpGet("{id}")]
        public async Task<ActionResult<TForm>> Read(int id)
        {
            TFormEntity formEntity = await ReadById(id);

            return Ok(Mapper.Map<TForm>(formEntity));
        }


        [HttpHead]
        public async Task GetPaggingMetadata([FromQuery] QueryTransformArgs transformArgs, [FromQuery] PaggingArgs paggingArgs)
        {
            QueryBuilder.AddDataTransformation(transformArgs);

            var metadata = await Mediator.Send(new GetMetadataQuery(paggingArgs, QueryBuilder));

            HttpContext.Response.Headers.Add(metadata);
        }

        [HttpPost] 
        public async Task<IActionResult> Create(TForm entityForm) 
        {
            var formEntity = Mapper.Map<TFormEntity>(entityForm);

            var createdEntity = await Mediator.Send(new CreateCommand(formEntity));

            return CreatedAtAction(nameof(Read), new { id = createdEntity.Id }, Mapper.Map<TForm>(createdEntity));
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TForm entityForm, [FromServices] DbFormEntityQueryBuilder<Book> queryBuilder)
        {
            TFormEntity formEntity = await ReadById(id);
            
            formEntity = Mapper.Map(entityForm, formEntity);

            await Mediator.Send(new UpdateCommand(id, formEntity));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedFormEntity = (IFormEntity) await Mediator.Send(new GetByIdQuery(id, QueryBuilder));
            await Mediator.Send(new DeleteCommand(deletedFormEntity));

            return NoContent();
        }
    }
}
