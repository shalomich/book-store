using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.Tags.GetTagGroups;

public record GetTagGroupsQuery() : IRequest<IEnumerable<TagGroupDto>>;
internal class GetTagGroupsQueryHandler : IRequestHandler<GetTagGroupsQuery, IEnumerable<TagGroupDto>>
{
    public ApplicationContext Context { get; }
    public IMapper Mapper { get; }

    public GetTagGroupsQueryHandler(
        ApplicationContext context,
        IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public async Task<IEnumerable<TagGroupDto>> Handle(GetTagGroupsQuery request, CancellationToken cancellationToken)
    {
        return await Context.Set<TagGroup>()
            .ProjectTo<TagGroupDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

