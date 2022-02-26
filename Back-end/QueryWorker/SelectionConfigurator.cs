using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker
{
    public class SelectionConfigurator<T> where T : class
    {
        private QueryConfiguration<T> Configuration { get; }
        public SelectionConfigurator(ConfigurationFinder configurationFinder)
        {
            Configuration = configurationFinder.Find<T>();
        }

        public IQueryable<T> AddFilters(IQueryable<T> query, FilterArgs[] filterArgArray)
        {
            if (filterArgArray == null)
                throw new ArgumentNullException(nameof(filterArgArray));

            foreach (var filterArgs in filterArgArray)
            {
                Filter<T> filter = Configuration.GetFilter(filterArgs.PropertyName)
                    with
                { ComparedValue = filterArgs.ComparedValue };

                query = filter.Transform(query);
            }

            return query;
        }

        public IQueryable<T> AddSorting(IQueryable<T> query, SortingArgs[] sortingArgsArray)
        {
            if (sortingArgsArray == null)
                throw new ArgumentNullException(nameof(sortingArgsArray));

            foreach (var sortingArgs in sortingArgsArray)
            {
                Sorting<T> sorting = Configuration.GetSorting(sortingArgs.PropertyName)
                    with
                { IsAscending = sortingArgs.IsAscending };

                query = sorting.Transform(query);
            }

            return query;
        }

        public IQueryable<T> AddSearch(IQueryable<T> query, SearchArgs searchArgs)
        {
            if (searchArgs == null)
                throw new ArgumentNullException(nameof(searchArgs));

            Search<T> search = Configuration.GetSearch(searchArgs.PropertyName)
                with
            { ComparedValue = searchArgs.ComparedValue, SearchDepth = searchArgs.SearchDepth };

            return search.Transform(query);
        }

        public IQueryable<T> AddPagging(IQueryable<T> query, PaggingArgs paggingArgs)
        {
            if (paggingArgs == null)
                throw new ArgumentNullException(nameof(paggingArgs));

            var pagging = new Pagging<T>
            {
                PageSize = paggingArgs.PageSize,
                PageNumber = paggingArgs.PageNumber
            };

            return pagging.Transform(query);
        }
    }
}

