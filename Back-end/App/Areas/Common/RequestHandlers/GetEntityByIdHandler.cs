using App.Entities;
using App.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetEntityByIdHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetEntityByIdHandler : IRequestHandler<GetByIdQuery, IEntity>
    {
        public record GetByIdQuery(int Id, Type EntityType) : IRequest<IEntity>;
        private ApplicationContext Context { get; }

        private const string WrongIdMessage = "Entity does not exist by this id";

        public GetEntityByIdHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEntity> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var (id, entityType) = request;

            var entity = (IEntity)await Context.FindAsync(entityType, id);

            if (entity == null)
                throw new NotFoundException(WrongIdMessage);

            return entity;
        }
    }
}
