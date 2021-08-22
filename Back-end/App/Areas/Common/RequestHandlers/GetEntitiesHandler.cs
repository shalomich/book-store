using App.Entities;
using App.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetEntitiesHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetEntitiesHandler : IRequestHandler<GetEntitiesQuery, IEnumerable<IEntity>>
    {
        public record GetEntitiesQuery(Type EntityType, PaggingArgs Args) : IRequest<IEnumerable<IEntity>>;
        private ApplicationContext Context { get; }

        public GetEntitiesHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<IEntity>> Handle(GetEntitiesQuery request, CancellationToken cancellationToken)
        {
            var (entityType, args) = request;

            return await Context.Entities(entityType,args).ToListAsync();
        }
    }
}
