using BookStore.Domain.Entities;
using App.QueryConfigs;
using App.Services.QueryBuilders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetHandler : IRequestHandler<GetQuery, IEnumerable<IEntity>>
    {
        public record GetQuery(IDbQueryBuilder<IEntity> QueryBuilder) : IRequest<IEnumerable<IEntity>>;

        public async Task<IEnumerable<IEntity>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            return await request.QueryBuilder.Build().ToListAsync();
        }
    }
}
