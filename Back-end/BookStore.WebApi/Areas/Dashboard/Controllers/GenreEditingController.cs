using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/genre")]
public class GenreEditingController : RelatedEntityEditingController<Genre, RelatedEntityForm>
{
    public GenreEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Genre> queryBuilder) : base(mediator, mapper, queryBuilder)
    {
    }
}

