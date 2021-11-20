using BookStore.Domain.Entities;
using QueryWorker;
using QueryWorker.Args;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Persistance;
using BookStore.Application.Extensions;
using BookStore.Application.Services.DbQueryBuilders.DbQueryBuildItems;
using Abp.Specifications;

namespace BookStore.Application.Services.DbQueryBuilders
{
    public class DbFormEntityQueryBuilder<T> : DbEntityQueryBuilder<T> where T : class, IFormEntity
    {
        private DataTransformerBuildFacade<T> BuildFacade { get; }
        public DbFormEntityQueryBuilder(ApplicationContext context, DataTransformerBuildFacade<T> transformerFacade) : base(context)
        {
            BuildFacade = transformerFacade;
        }

        public DbFormEntityQueryBuilder<T> AddSpecification(ISpecification<T> specification)
        {
            AddBuildItem(new SpecificationBuildItem<T>(specification));

            return this;
        }
        public DbFormEntityQueryBuilder<T> AddDataTransformation(QueryTransformArgs args)
        {
            if (args == null || args.IsQueryEmpty)
                return this;

            foreach (var filterArgs in args.Filters.EmptyIfNull())
                AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildFilter(filterArgs)));

            foreach (var sortingArgs in args.Sortings.EmptyIfNull())
                AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildSorting(sortingArgs)));

            foreach (var searchArgs in args.Searches.EmptyIfNull())
                AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildSearch(searchArgs)));

            return this;
        }

        public DbFormEntityQueryBuilder<T> AddPagging(PaggingArgs args)
        {
            AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildPagging(args)));

            return this;
        }

        private DataTransformerBuildItem<T> CreateDataTransformerBuildItem(Func<IDataTransformer<T>> transformerBuild) 
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
