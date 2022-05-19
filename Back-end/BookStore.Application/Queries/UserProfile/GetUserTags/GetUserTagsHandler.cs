using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Dto;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.UserProfile;

public record GetUserTagsQuery() : IRequest<IEnumerable<RelatedEntityDto>>;
internal class GetUserTagsHandler : IRequestHandler<GetUserTagsQuery, IEnumerable<RelatedEntityDto>>
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public GetUserTagsHandler(
        ApplicationContext context, 
        IMapper mapper,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        Mapper = mapper;
        LoggedUserAccessor = loggedUserAccessor;
    }

    public async Task<IEnumerable<RelatedEntityDto>> Handle(GetUserTagsQuery request, CancellationToken cancellationToken)
    {
        return await Context.Tags
            .Where(tag => tag.Users.Any(user => user.Id == LoggedUserAccessor.GetCurrentUserId()))
            .ProjectTo<RelatedEntityDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
