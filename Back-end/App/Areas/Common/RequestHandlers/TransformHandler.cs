using App.Areas.Common.ViewModels;
using App.Entities;
using App.Entities.Publications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.TransformHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class TransformHandler : IRequestHandler<TransformQuery, FormEntitiesByQuery>
    {
        public record TransformQuery(IQueryable<FormEntity> FormEntities, QueryTransformArgs Args) : IRequest<FormEntitiesByQuery>;

        private DataTransformerFacade DataTransformer { get; }

        public TransformHandler(DataTransformerFacade dataTransformer)
        {
            DataTransformer = dataTransformer ?? throw new ArgumentNullException(nameof(dataTransformer));
        }

        public Task<FormEntitiesByQuery> Handle(TransformQuery request, CancellationToken cancellationToken)
        {
            var (formEntities, args) = request;

            Type formEntityType = formEntities.GetType()
                .GetGenericArguments()
                .First();

            formEntities = formEntities.AsQueryable();

            return Task.Run(() =>
            {
                formEntities = formEntityType.Name switch
                {
                    nameof(Author) => DataTransformer.Transform((IQueryable<Author>)formEntities, args),
                    nameof(Publisher) => DataTransformer.Transform((IQueryable<Publisher>)formEntities, args),
                    nameof(Genre) => DataTransformer.Transform((IQueryable<Genre>)formEntities, args),
                    nameof(PublicationType) => DataTransformer.Transform((IQueryable<PublicationType>)formEntities, args),
                    nameof(AgeLimit) => DataTransformer.Transform((IQueryable<AgeLimit>)formEntities, args),
                    nameof(CoverArt) => DataTransformer.Transform((IQueryable<CoverArt>)formEntities, args),
                    nameof(Publication) => DataTransformer.Transform((IQueryable<Publication>)formEntities, args),
                    _ => throw new ArgumentException()
                };

                return new FormEntitiesByQuery 
                { 
                    FormEntities = formEntities, 
                    QueryErrors = DataTransformer.ErrorMesages 
                };
            });
        }
    }
}
