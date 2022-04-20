using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.Order.GetOrders;

public record GetOrdersQuery() : IRequest<IEnumerable<OrderDto>>;
internal class GetOrdersHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private LoggedUserAccessor LoggedUserAccessor { get; }
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }

    public GetOrdersHandler(LoggedUserAccessor loggedUserAccessor, ApplicationContext context, IMapper mapper)
    {
        LoggedUserAccessor = loggedUserAccessor;
        Context = context;
        Mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        return await Context.Orders
            .Where(order => order.UserId == currentUserId)
            .ProjectTo<OrderDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

