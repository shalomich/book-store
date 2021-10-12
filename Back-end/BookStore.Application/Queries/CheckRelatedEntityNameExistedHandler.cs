using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Exceptions;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;

namespace BookStore.Application.Queries
{
    public record CheckRelatedEntityNameQuery(string Name, IDbQueryBuilder<RelatedEntity> QueryBuilder) : IRequest<bool>;
    public class CheckRelatedEntityNameExistedHandler : IRequestHandler<CheckRelatedEntityNameQuery, bool>
    {
        private const string EmptyNameMessage = "Name is empty";

        public Task<bool> Handle(CheckRelatedEntityNameQuery request, CancellationToken cancellationToken)
        {
            var (name, builder) = request;

            if (string.IsNullOrEmpty(name))
                throw new BadRequestException(EmptyNameMessage);

            bool isExisted = builder.Build().Any(relatedEntity => relatedEntity.Name == name);

            return Task.FromResult(isExisted);
        }
    }
}
