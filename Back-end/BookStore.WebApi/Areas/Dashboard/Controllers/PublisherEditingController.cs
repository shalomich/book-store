using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/publisher")]
public class PublisherEditingController : RelatedEntityEditingController<Publisher, RelatedEntityForm>
{
    public PublisherEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<Publisher> queryBuilder) : base(mediator, mapper, queryBuilder)
    {
    }
}

