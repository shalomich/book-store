using BookStore.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.Queries;
using BookStore.WebApi.Extensions;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Commands.RelatedEntityEditing.CreateRelatedEntity;
using System.Threading;
using BookStore.Application.Commands.RelatedEntityEditing.UpdateRelatedEntity;
using BookStore.Application.Commands.RelatedEntityEditing.DeleteRelatedEntity;
using BookStore.Application.Commands.RelatedEntityEditing.GetEditRelatedEntity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[ApiController]
[Area("dashboard")]
[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
public abstract class RelatedEntityEditingController<TRelatedEntity, TRelatedEntityForm> : Controller where TRelatedEntity : RelatedEntity
    where TRelatedEntityForm : RelatedEntityForm
{
    public IMediator Mediator { get; }
    public IMapper Mapper { get; }
    public DbFormEntityQueryBuilder<TRelatedEntity> QueryBuilder { get; }

    public RelatedEntityEditingController(
        IMediator mediator, 
        IMapper mapper, 
        DbFormEntityQueryBuilder<TRelatedEntity> queryBuilder)
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
    public async Task<ActionResult<TRelatedEntityForm[]>> GetRelatedEntities([FromQuery][Required] PaggingArgs pagging)
    {
        QueryBuilder
            .AddPagging(pagging);

        var relatedEntities = await Mediator.Send(new GetQuery(QueryBuilder));

        return relatedEntities
            .Select(relatedEntity => Mapper.Map<TRelatedEntityForm>(relatedEntity))
            .ToArray();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TRelatedEntityForm>> GetEditRelatedEntity(int id, CancellationToken cancellationToken)
    {
        return (TRelatedEntityForm) await Mediator.Send(new GetEditRelatedEntityQuery(id, typeof(TRelatedEntity)), cancellationToken);
    }

    [HttpPost]
    public async Task<int> CreateRelatedEntity(TRelatedEntityForm relatedEntityForm, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new CreateRelatedEntityCommand(typeof(TRelatedEntity), relatedEntityForm), cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRelatedEntity(int id, TRelatedEntityForm relatedEntityForm, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateRelatedEntityCommand(id, typeof(TRelatedEntity), relatedEntityForm), cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRelatedEntity(int id, CancellationToken cancellationToken)
    {     
        await Mediator.Send(new DeleteRelatedEntityCommand(id, typeof(TRelatedEntity)), cancellationToken);

        return NoContent();
    }

    [HttpGet("name-existed")]
    public async Task<bool> CheckNameExisted([FromQuery] string name)
    {
        return await Mediator.Send(new CheckRelatedEntityNameQuery(name, QueryBuilder));
    }
}

