using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/tagGroup")]
public class TagGroupEditingController : RelatedEntityEditingController<TagGroup, TagGroupForm>
{
    public TagGroupEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<TagGroup> queryBuilder) : base(mediator, mapper, queryBuilder)
    {
    }
}

