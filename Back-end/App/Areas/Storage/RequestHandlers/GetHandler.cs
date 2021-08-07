using App.Entities;
using App.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Storage.RequestHandlers.GetByIdHandler;
using static App.Areas.Storage.RequestHandlers.GetHandler;

namespace App.Areas.Storage.RequestHandlers
{
    public class GetHandler : IRequestHandler<GetQuery, IEnumerable<Entity>>
    {
        public record GetQuery(Type EntityType) : IRequest<IEnumerable<Entity>>;
        private ApplicationContext Context { get; }

        private const string WrongIdMessage = "Entity does not exist by this id";

        public GetHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Entity>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            return await Context.GetEntitiesAsync(request.EntityType);
        }
    }
}
