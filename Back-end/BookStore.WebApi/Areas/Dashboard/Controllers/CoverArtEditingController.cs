using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Areas.Dashboard.Controllers;

[Route("[area]/form-entity/coverArt")]
public class CoverArtEditingController : RelatedEntityEditingController<CoverArt, RelatedEntityForm>
{
    public CoverArtEditingController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<CoverArt> queryBuilder) : base(mediator, mapper, queryBuilder)
    {
    }
}

