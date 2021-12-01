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

        public DbFormEntityQueryBuilder<T> AddSortings(IEnumerable<SortingArgs> args)
        {
            if (args != null)
            {
                foreach (var sortingArgs in args)
                    AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildSorting(sortingArgs)));
            }

            return this;
        }
        public DbFormEntityQueryBuilder<T> AddFilters(IEnumerable<FilterArgs> args)
        {
            if (args != null)
            {
                foreach (var filterArgs in args)
                    AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildFilter(filterArgs)));
            }

            return this;
        }

        public DbFormEntityQueryBuilder<T> AddSearch(SearchArgs args)
        {
            AddBuildItem(CreateDataTransformerBuildItem(() => BuildFacade.BuildSearch(args)));

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
