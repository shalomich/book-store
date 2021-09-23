using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Persistance;
using BookStore.Application.Exceptions;

namespace BookStore.Application.Commands
{
    public record CreateCommand(IEntity Entity) : IRequest<IEntity>;
    public class CreateHandler : IRequestHandler<CreateCommand, IEntity>
    {    
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
                throw new BadRequestException(exception.InnerException.Message);
            }

            return createdEntity;
        }
    }
}
