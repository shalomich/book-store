using BookStore.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.Queries;
using BookStore.WebApi.Attributes.GenericController;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;
using BookStore.Application.Commands;
using BookStore.WebApi.Extensions;

namespace BookStore.WebApi.Areas.Dashboard.Controllers
{
    [Route("[area]/form-entity/[controller]")]
    [ApiController]
    [Area("dashboard")]
    [GenericController()]
    public class RelatedEntityEditingController<T> : Controller where T : RelatedEntity
    {
        public IMediator Mediator { get; }
        public IMapper Mapper { get; }
        public DbFormEntityQueryBuilder<T> QueryBuilder { get; }

        public RelatedEntityEditingController(
            IMediator mediator, 
            IMapper mapper, 
            DbFormEntityQueryBuilder<T> queryBuilder)
        {
            Mediator = mediator;
            Mapper = mapper;
            QueryBuilder = queryBuilder;
        }

        [HttpHead]
        public async Task GetPaggingMetadata([FromQuery] PaggingArgs paggingArgs)
        {
            var metadata = await Mediator.Send(new GetMetadataQuery(paggingArgs, QueryBuilder));

            HttpContext.Response.Headers.Add(metadata);
        }

        [HttpGet]
        public async Task<ActionResult<RelatedEntityForm[]>> Read([FromQuery] PaggingArgs paggingArgs)
        {
            QueryBuilder
                .AddPagging(paggingArgs);

            var relatedEntities = await Mediator.Send(new GetQuery(QueryBuilder));

            return relatedEntities
                .Select(relatedEntity => Mapper.Map<RelatedEntityForm>(relatedEntity))
                .ToArray();
        }

        protected async Task<T> ReadById(int id)
        {
            return (T) await Mediator.Send(new GetByIdQuery(id, QueryBuilder));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RelatedEntityForm>> Read(int id)
        {
            var formEntity = await ReadById(id);

            return Ok(Mapper.Map<RelatedEntityForm>(formEntity));
        }

        [HttpPost]
        public async Task<int> Create(RelatedEntityForm entityForm)
        {
            var formEntity = Mapper.Map<T>(entityForm);

            var createdEntity = await Mediator.Send(new CreateCommand(formEntity));

            return createdEntity.Id;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RelatedEntityForm entityForm)
        {
            var formEntity = await ReadById(id);

            formEntity = Mapper.Map(entityForm, formEntity);

            await Mediator.Send(new UpdateCommand(id, formEntity));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedFormEntity = await Mediator.Send(new GetByIdQuery(id, QueryBuilder));
            
            await Mediator.Send(new DeleteCommand(deletedFormEntity));

            return NoContent();
        }

        [HttpGet("name-existed")]
        public async Task<bool> CheckNameExisted([FromQuery] string name)
        {
            return await Mediator.Send(new CheckRelatedEntityNameQuery(name, QueryBuilder));
        }
    }
}
