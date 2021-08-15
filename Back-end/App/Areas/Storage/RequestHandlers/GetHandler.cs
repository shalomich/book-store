using App.Entities;
using App.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Storage.RequestHandlers.GetHandler;

namespace App.Areas.Storage.RequestHandlers
{
    public class GetHandler : IRequestHandler<GetQuery, IEnumerable<Entity>>
    {
        public record GetQuery(Type EntityType, QueryArgs QueryParams) : IRequest<IEnumerable<Entity>>;
        private ApplicationContext Context { get; }
   
        public GetHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Entity>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var (entityType, queryParams) = request;
            
            return await Context.Entities(entityType, queryParams).ToListAsync();
        }
    }
}
