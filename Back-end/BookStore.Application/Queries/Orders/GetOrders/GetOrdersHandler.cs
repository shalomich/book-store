using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Extensions;
using BookStore.Application.Services;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.Orders.GetOrders;

public record GetOrdersQuery(PaggingArgs Pagging) : IRequest<IEnumerable<OrderDto>>;
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
        var pagging = request.Pagging;

        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        return await Context.Orders
            .Where(order => order.UserId == currentUserId)
            .OrderByDescending(order => order.PlacedDate)
            .Paginate(pagging.PageSize, pagging.PageNumber)
            .ProjectTo<OrderDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

