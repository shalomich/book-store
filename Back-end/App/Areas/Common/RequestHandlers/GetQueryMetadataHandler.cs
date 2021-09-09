using App.Entities;
using App.Entities.Books;
using App.Exceptions;
using MediatR;
using QueryWorker;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetQueryMetadataHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetQueryMetadataHandler : IRequestHandler<GetMetadataQuery, QueryMetadata>
    {
        public record GetMetadataQuery(IQueryable<FormEntity> FormEntities, QueryTransformArgs Args) : IRequest<QueryMetadata>;

        private DataTransformerFacade DataTransformer { get; }

        public GetQueryMetadataHandler(DataTransformerFacade dataTransformer)
        {
            DataTransformer = dataTransformer ?? throw new ArgumentNullException(nameof(dataTransformer));
        }
        public Task<QueryMetadata> Handle(GetMetadataQuery request, CancellationToken cancellationToken)
        {
            var (formEntities, args) = request;

            Type formEntityType = formEntities.GetType()
                .GetGenericArguments()
                .First();

            QueryMetadata metadata = null;

            try
            {
                metadata = formEntityType.Name switch
                {
                    nameof(Author) => DataTransformer.GetQueryMetadata((IQueryable<Author>)formEntities, args),
                    nameof(Publisher) => DataTransformer.GetQueryMetadata((IQueryable<Publisher>)formEntities, args),
                    nameof(Genre) => DataTransformer.GetQueryMetadata((IQueryable<Genre>)formEntities, args),
                    nameof(BookType) => DataTransformer.GetQueryMetadata((IQueryable<BookType>)formEntities, args),
                    nameof(AgeLimit) => DataTransformer.GetQueryMetadata((IQueryable<AgeLimit>)formEntities, args),
                    nameof(CoverArt) => DataTransformer.GetQueryMetadata((IQueryable<CoverArt>)formEntities, args),
                    nameof(Book) => DataTransformer.GetQueryMetadata((IQueryable<Book>)formEntities, args),
                    _ => throw new ArgumentException()
                };
            }
            catch (Exception exception)
            {
                throw new BadRequestException(exception.Message);
            }

            return Task.FromResult(metadata);
        }
    }
}
