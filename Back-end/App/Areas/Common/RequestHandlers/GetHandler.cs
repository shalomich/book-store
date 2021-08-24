using App.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetHandler : IRequestHandler<GetQuery, IQueryable<IEntity>>
    {
        public record GetQuery(Type EntityType) : IRequest<IQueryable<IEntity>>;
        private ApplicationContext Context { get; }

        public GetHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IQueryable<IEntity>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            Type entityType = request.EntityType;

            if (typeof(IEntity).IsAssignableFrom(entityType) == false)
                throw new ArgumentException();

            return await Task.Run(() => 
            {
                return (IQueryable<IEntity>)Context
                .GetType()
                .GetMethods()
                .SingleOrDefault(method => method.Name == nameof(Context.Set)
                    && method.GetParameters().Count() == 0)
                .MakeGenericMethod(entityType)
                .Invoke(Context, null);
            });
        }
    }
}
