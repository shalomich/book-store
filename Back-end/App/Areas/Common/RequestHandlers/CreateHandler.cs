using App.Entities;
using App.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.CreateHandler;


namespace App.Areas.Common.RequestHandlers
{
    public class CreateHandler : IRequestHandler<CreateCommand, IEntity>
    {
        public record CreateCommand(IEntity Entity) : IRequest<FormEntity>;
        private ApplicationContext Context { get; }

        public CreateHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEntity> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var createdEntity = request.Entity;

            try
            {
                await Context.AddAsync(createdEntity);
                await Context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new BadRequestException("Wrong data", exception);
            }

            return createdEntity;
        }
    }
}
