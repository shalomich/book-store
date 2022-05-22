using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/bookType")]
public class BookTypeEditingController : RelatedEntityEditingController<BookType, RelatedEntityForm>
{
    public BookTypeEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<BookType> queryBuilder) : base(mediator, mapper, queryBuilder)
    {
    }
}

