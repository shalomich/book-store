using App.Areas.Common.ViewModels;
using App.Areas.Dashboard.ViewModels;
using App.Entities;
using App.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using QueryWorker.Args;
using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetByQueryHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetByQueryHandler : IRequestHandler<GetByQuery, ValidQueryData<IEnumerable<Entity>>>
    {
        public record GetByQuery(Type EntityType, QueryArgs QueryParams) : IRequest<ValidQueryData<IEnumerable<Entity>>>;
        private ApplicationContext Context { get; }
   
        public GetByQueryHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ValidQueryData<IEnumerable<Entity>>> Handle(GetByQuery request, CancellationToken cancellationToken)
        {
            var (entityType, queryParams) = request;

            var data = await Context.EntitiesByQuery(entityType, queryParams).ToListAsync();

            return new ValidQueryData<IEnumerable<Entity>>(data, Context.DataTransformer.ErrorMesages);
        }
    }

    

    
  
    

    
}
