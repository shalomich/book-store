﻿using App.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Storage.RequestHandlers.GetByIdHandler;

namespace App.Areas.Storage.RequestHandlers
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, Entity>
    {
        public record GetByIdQuery(int Id, Type EntityType) : IRequest<Entity>;
        private ApplicationContext Context { get; }

        private const string WrongIdMessage = "Entity does not exist by this id";

        public GetByIdHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Entity> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var (id, entityType) = request;

            var entity = (Entity)await Context.FindAsync(entityType, id);

            if (entity == null)
                throw new ArgumentException();

            return entity;
        }
    }
}
