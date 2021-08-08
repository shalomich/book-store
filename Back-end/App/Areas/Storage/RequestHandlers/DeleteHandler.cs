using App.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Storage.RequestHandlers.DeleteHandler;

namespace App.Areas.Storage.RequestHandlers
{
    public class DeleteHandler : IRequestHandler<DeleteCommand, Unit>
    {
        public record DeleteCommand(Entity Entity) : IRequest<Unit>;
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
