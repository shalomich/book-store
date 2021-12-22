using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Exceptions;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record GetByIdQuery(int Id, IDbQueryBuilder<IEntity> QueryBuilder) : IRequest<IEntity>;
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, IEntity>
    {     
        private const string WrongIdMessage = "Entity does not exist by this id";
        public async Task<IEntity> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var (id, queryBuilder) = request;

            var entity = await queryBuilder.Build()
                .SingleOrDefaultAsync(new EntityByIdSpecification(id));

            if (entity == null)
                throw new NotFoundException(WrongIdMessage);

            return entity;
        }
    }
}
