using App.Areas.Common.ViewModels;
using App.Entities;
using App.Entities.Books;
using App.Exceptions;
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
    public class TransformHandler : IRequestHandler<TransformQuery, IQueryable<IFormEntity>>
    {
        public record TransformQuery(IQueryable<IFormEntity> FormEntities, QueryTransformArgs Args) : IRequest<IQueryable<IFormEntity>>;

        private DataTransformerFacade DataTransformer { get; }

        public TransformHandler(DataTransformerFacade dataTransformer)
        {
            DataTransformer = dataTransformer ?? throw new ArgumentNullException(nameof(dataTransformer));
        }

        public Task<IQueryable<IFormEntity>> Handle(TransformQuery request, CancellationToken cancellationToken)
        {
            var (formEntities, args) = request;

            Type IFormEntityType = formEntities.GetType()
                .GetGenericArguments()
                .First();

            try
            {
                formEntities = IFormEntityType.Name switch
                {
                    nameof(Author) => DataTransformer.Transform((IQueryable<Author>)formEntities, args),
                    nameof(Publisher) => DataTransformer.Transform((IQueryable<Publisher>)formEntities, args),
                    nameof(Genre) => DataTransformer.Transform((IQueryable<Genre>)formEntities, args),
                    nameof(BookType) => DataTransformer.Transform((IQueryable<BookType>)formEntities, args),
                    nameof(AgeLimit) => DataTransformer.Transform((IQueryable<AgeLimit>)formEntities, args),
                    nameof(CoverArt) => DataTransformer.Transform((IQueryable<CoverArt>)formEntities, args),
                    nameof(Book) => DataTransformer.Transform((IQueryable<Book>)formEntities, args),
                    _ => throw new ArgumentException()
                };
            }
            catch(Exception exception)
            {
                throw new BadRequestException(exception.Message);
            }
           
            return Task.FromResult(formEntities);
        }
    }
}
