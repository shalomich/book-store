using App.Entities;
using App.Extensions;
using QueryWorker;
using QueryWorker.Args;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.QueryBuilders
{
    public class DbFormEntityQueryBuilder<T> : DbEntityQueryBuilder<T> where T : class, IFormEntity
    {
        private DataTransformerBuildFacade<T> BuildFacade { get; }
        public DbFormEntityQueryBuilder(ApplicationContext context, DataTransformerBuildFacade<T> transformerFacade) : base(context)
        {
            BuildFacade = transformerFacade;
        }

        public DbFormEntityQueryBuilder<T> AddDataTransformation(QueryTransformArgs args)
        {
            if (args == null)
                return this;

            foreach (var filterArgs in args.Filters.EmptyIfNull())
                AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildFilter(filterArgs)));
            
            foreach (var sortingArgs in args.Sortings.EmptyIfNull())
                AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildSorting(sortingArgs)));

            foreach (var searchArgs in args.Searches.EmptyIfNull())
                AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildSearch(searchArgs)));

            AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildPagging(args.Pagging)));

            return this;
        }

        private DataTransformerBuildItem<T> CreateDataTransformerBuildItem(Func<DataTransformer<T>> transformerBuild) 
        {
            try
            {
                return new DataTransformerBuildItem<T>(transformerBuild());
            }
            catch (Exception)
            {
                return null;
            }
        } 


    }
}
