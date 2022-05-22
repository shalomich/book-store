using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/ageLimit")]
public class AgeLimitEditingController : RelatedEntityEditingController<AgeLimit, RelatedEntityForm>
{
    public AgeLimitEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<AgeLimit> queryBuilder) : base(mediator, mapper, queryBuilder)
    {
    }
}

