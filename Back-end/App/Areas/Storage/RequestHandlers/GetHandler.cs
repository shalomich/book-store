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
        public record GetQuery(Type EntityType, QueryParams QueryParams) : IRequest<IEnumerable<Entity>>;
        private ApplicationContext Context { get; }
        private QueryTransformer QueryTransformer { get; }

        private const string WrongIdMessage = "Entity does not exist by this id";

        public GetHandler(ApplicationContext context, QueryTransformer queryTransformer)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            QueryTransformer = queryTransformer ?? throw new ArgumentNullException(nameof(queryTransformer));
        }

        public async Task<IEnumerable<Entity>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var (entityType, queryParams) = request;
            
            return await QueryTransformer.Transform(Context.Entities(entityType).AsQueryable(), queryParams)
                .ToListAsync();
        }
    }
}
