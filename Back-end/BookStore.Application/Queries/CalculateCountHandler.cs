using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record CalculateCountQuery(IDbQueryBuilder<IEntity> QueryBuilder) : IRequest<int>;
    internal class CalculateCountHandler : IRequestHandler<CalculateCountQuery, int>
    {
        public Task<int> Handle(CalculateCountQuery request, CancellationToken cancellationToken)
        {
            return request.QueryBuilder
                .Build()
                .CountAsync();
        }
    }

}
