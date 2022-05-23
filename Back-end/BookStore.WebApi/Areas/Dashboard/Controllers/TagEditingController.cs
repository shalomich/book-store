using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/tag")]
public class TagEditingController : RelatedEntityEditingController<Tag, TagForm>
{
    public TagEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Tag> queryBuilder) : base(mediator, mapper, queryBuilder)
    {
    }
}

