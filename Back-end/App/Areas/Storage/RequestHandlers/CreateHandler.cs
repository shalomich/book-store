using App.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Storage.RequestHandlers.CreateHandler;

namespace App.Areas.Storage.RequestHandlers
{
    public class CreateHandler : IRequestHandler<CreateCommand, Entity>
    {
        public record CreateCommand(Entity Entity) : IRequest<Entity>;
        private ApplicationContext Context { get; }

        public CreateHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Entity> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            Entity createdEntity = request.Entity;

            try
            {
                await Context.AddAsync(createdEntity);
                await Context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return createdEntity;
        }
    }
}
