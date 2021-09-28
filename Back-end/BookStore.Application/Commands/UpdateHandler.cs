﻿using BookStore.Domain.Entities;
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
    public record UpdateCommand(int Id, IEntity Entity) : IRequest<Unit>;

    public class UpdateHandler : IRequestHandler<UpdateCommand, Unit>
    {
        
        private const string WrongIdMessage = "Id from route and body are different";

        private ApplicationContext Context { get; }

        public UpdateHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var (id, entity) = request;

            if (entity.Id != id)
            {
                throw new BadRequestException(WrongIdMessage);
            }

            try
            {
                Context.Update(entity);
                await Context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new BadRequestException(exception.InnerException.Message);
            }

            return Unit.Value;
        }
    }
}