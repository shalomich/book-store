using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/author")]
public class AuthorEditingController : RelatedEntityEditingController<Author, AuthorForm>
{
    public AuthorEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Author> queryBuilder) : base(mediator, mapper, queryBuilder)
    {
    }
}

