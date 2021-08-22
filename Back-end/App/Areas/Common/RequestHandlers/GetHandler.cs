using App.Entities;
using App.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetHandler : IRequestHandler<GetQuery, IEnumerable<Entity>>
    {
        public record GetQuery(Type EntityType) : IRequest<IEnumerable<Entity>>;
        private ApplicationContext Context { get; }

        public GetHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Entity>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            return await Context.Entities(request.EntityType).ToListAsync();
        }
    }
}
