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
using static App.Areas.Common.RequestHandlers.GetFormEntitiesHandler;

namespace App.Areas.Common.RequestHandlers
{
    public class GetFormEntitiesHandler : IRequestHandler<GetFormEntitiesQuery, ValidQueryData<IEnumerable<FormEntity>>>
    {
        public record GetFormEntitiesQuery(Type FormEntityType, QueryArgs Args) : IRequest<ValidQueryData<IEnumerable<FormEntity>>>;
        private ApplicationContext Context { get; }
   
        public GetFormEntitiesHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ValidQueryData<IEnumerable<FormEntity>>> Handle(GetFormEntitiesQuery request, CancellationToken cancellationToken)
        {
            var (entityType, args) = request;

            var data = await Context.FormEntities(entityType, args).ToListAsync();

            return new ValidQueryData<IEnumerable<FormEntity>>(data, Context.DataTransformer.ErrorMesages);
        }
    }

    

    
  
    

    
}
