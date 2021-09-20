using App.Entities;
using App.Exceptions;
using App.Services.QueryBuilders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetByIdHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, IEntity>
    {
        public record GetByIdQuery(int Id, IDbQueryBuilder<IEntity> QueryBuilder) : IRequest<IEntity>;
       
        private const string WrongIdMessage = "Entity does not exist by this id";

        public async Task<IEntity> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var (id, queryBuilder) = request;

            var entity = await queryBuilder.Build()
                .SingleOrDefaultAsync(entity => entity.Id == id);

            if (entity == null)
                throw new NotFoundException(WrongIdMessage);

            return entity;
        }
    }
}
