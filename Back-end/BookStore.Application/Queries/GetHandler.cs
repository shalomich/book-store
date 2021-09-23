using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record GetQuery(IDbQueryBuilder<IEntity> QueryBuilder) : IRequest<IEnumerable<IEntity>>;
    public class GetHandler : IRequestHandler<GetQuery, IEnumerable<IEntity>>
    {        
        public async Task<IEnumerable<IEntity>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            return await request.QueryBuilder.Build().ToListAsync();
        }
    }
}
