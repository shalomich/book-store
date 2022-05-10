using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters;
using QueryWorker.DataTransformers.Paggings;

namespace QueryWorker
{
    public class DataTransformerBuildFacade<T> where T : class
    {
        private QueryConfiguration<T> Configuration { get; }
        public DataTransformerBuildFacade(ConfigurationFinder configurationFinder)
        {
            Configuration = configurationFinder.Find<T>();
        }

        public IDataTransformer<T> BuildFilter(FilterArgs args)
        {
            Filter<T> filter = Configuration.GetFilter(args.PropertyName);

            return filter with { ComparedValue = args.ComparedValue};
        }

        public IDataTransformer<T> BuildSorting(SortingArgs args)
        {
            Sorting<T> sorting = Configuration.GetSorting(args.PropertyName);

            return sorting with { IsAscending = args.IsAscending };
        }

        public IDataTransformer<T> BuildSearch(SearchArgs args)
        {
            Search<T> search = Configuration.GetSearch(args.PropertyName);

            return search with { ComparedValue = args.ComparedValue, SearchDepth = args.SearchDepth };
        }

        public IDataTransformer<T> BuildPagging(PaggingArgs args)
        {
            return new Pagging<T>
            {
                PageSize = args.PageSize,
                PageNumber = args.PageNumber
            };
        }
    }
}
