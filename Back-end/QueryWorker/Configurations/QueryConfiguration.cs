using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QueryWorker.Extensions;
using QueryWorker.DataTransformers.Filters.ExpressionCreator;

namespace QueryWorker.Configurations
{
    public abstract class QueryConfiguration<T> where T : class
    {
        private readonly Dictionary<string, Sorting<T>> _sortings = new Dictionary<string, Sorting<T>>();
        private readonly Dictionary<string, Search<T>> _searches = new Dictionary<string, Search<T>>();
        private readonly Dictionary<string, Filter<T>> _filters = new Dictionary<string, Filter<T>>();

        internal Sorting<T> GetSorting(string propertyName) => _sortings[propertyName];
        internal Filter<T> GetFilter(string propertyName) 
            => _filters[propertyName];
        internal Search<T> GetSearch(string propertyName) => _searches[propertyName];
        
        protected void CreateSorting(string propertyName,Expression<Func<T,object>> propertySelector)
        {
            var sorting = new Sorting<T>(propertySelector);

            _sortings.Add(propertyName.ToLowFirstLetter(), sorting);
        }

        protected void CreateRangeFilter(string propertyName,Expression<Func<T, int>> propertySelector)
        {
            _filters.Add(key: propertyName.ToLowFirstLetter(),
                value: new Filter<T>(new RangeExpressionCreator<T>(propertySelector)));
        }

        protected void CreatePlentyFilter(string propertyName, Expression<Func<T, IEnumerable<int>>> propertySelector)
        {
            _filters.Add(key: propertyName.ToLowFirstLetter(),
                value: new Filter<T>(new PlentyForCollectionExpressionCreator<T>(propertySelector)));
        }

        protected void CreatePlentyFilter(string propertyName, Expression<Func<T, int>> propertySelector)
        {
            _filters.Add(key: propertyName.ToLowFirstLetter(),
                value: new Filter<T>(new PlentyForSingleExpressionCreator<T>(propertySelector)));
        }
        protected void CreateSearch(string propertyName, Expression<Func<T, string>> propertySelector)
        {
            _searches.Add(propertyName.ToLowFirstLetter(), new Search<T>(propertySelector));
        }
    }
}
