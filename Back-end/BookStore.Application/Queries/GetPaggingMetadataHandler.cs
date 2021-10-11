using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using MediatR;
using QueryWorker;
using QueryWorker.Args;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record GetMetadataQuery(int dataCount, PaggingArgs Args, IDbQueryBuilder<IFormEntity> QueryBuilder) : IRequest<PaggingMetadata>;

    public class GetPaggingMetadataHandler : IRequestHandler<GetMetadataQuery, PaggingMetadata>
    {        
        private PaggingMetadataCollector<IFormEntity> Collector { get; }

        public GetPaggingMetadataHandler(PaggingMetadataCollector<IFormEntity> collector)
        {
            Collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }

        public Task<PaggingMetadata> Handle(GetMetadataQuery request, CancellationToken cancellationToken)
        {
            var (dataCount, args, queryBuilder) = request;

            return Task.FromResult(Collector.Collect(dataCount, args, queryBuilder.Build()));
        }
    }
}
