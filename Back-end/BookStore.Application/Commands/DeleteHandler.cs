using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands
{
    public record DeleteCommand(IEntity Entity) : IRequest<Unit>;
    public class DeleteHandler : IRequestHandler<DeleteCommand, Unit>
    {
        private ApplicationContext Context { get; }
        public DeleteHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {   
            Context.Remove(request.Entity);
            await Context.SaveChangesAsync();
           
            return Unit.Value;
        }
    }
}
