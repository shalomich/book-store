using App.Entities;
using App.Entities.Books;
using App.Exceptions;
using App.Services.QueryBuilders;
using MediatR;
using QueryWorker;
using QueryWorker.Args;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetPaggingMetadataHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetPaggingMetadataHandler : IRequestHandler<GetMetadataQuery, PaggingMetadata>
    {
        public record GetMetadataQuery(PaggingArgs Args, IDbQueryBuilder<IFormEntity> QueryBuilder) : IRequest<PaggingMetadata>;

        private PaggingMetadataCollector<IFormEntity> Collector { get; }

        public GetPaggingMetadataHandler(PaggingMetadataCollector<IFormEntity> collector)
        {
            Collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }

        public Task<PaggingMetadata> Handle(GetMetadataQuery request, CancellationToken cancellationToken)
        {
            var (args, queryBuilder) = request;

            return Task.FromResult(Collector.Collect(args, queryBuilder.Build()));
        }
    }
}
