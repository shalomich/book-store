using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Dto;
using BookStore.Application.Exceptions;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Profile;

public record GetUserTagsQuery(int UserId) : IRequest<IEnumerable<RelatedEntityDto>>;
internal class GetUserTagsHandler : IRequestHandler<GetUserTagsQuery, IEnumerable<RelatedEntityDto>>
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }

    public GetUserTagsHandler(ApplicationContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public async Task<IEnumerable<RelatedEntityDto>> Handle(GetUserTagsQuery request, CancellationToken cancellationToken)
    {
        return await Context.Tags
            .Where(tag => tag.Users.Any(user => user.Id == request.UserId))
            .ProjectTo<RelatedEntityDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
