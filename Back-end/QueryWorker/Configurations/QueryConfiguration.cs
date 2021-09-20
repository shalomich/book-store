
using QueryWorker.Extensions;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using QueryWorker.Args;
using System.Linq;
using QueryWorker.DataTransformers.Paggings;

namespace QueryWorker.Configurations
{
    public abstract class QueryConfiguration<TConfigured> where TConfigured : class
    {
        private const int MaxPageNumber = 60;

        private Pagging<TConfigured> _pagging;

        private readonly Dictionary<string, DataTransformer<TConfigured>> _sortings = new Dictionary<string, DataTransformer<TConfigured>>();
        private readonly Dictionary<string, DataTransformer<TConfigured>> _filters = new Dictionary<string, DataTransformer<TConfigured>>();
        private readonly Dictionary<string, DataTransformer<TConfigured>> _searches = new Dictionary<string, DataTransformer<TConfigured>>();

        internal DataTransformer<TConfigured> GetSorting(string propertyKey) => _sortings[propertyKey];
        internal DataTransformer<TConfigured> GetFilter(string propertyKey) => _filters[propertyKey];
        internal DataTransformer<TConfigured> GetSearch(string propertyKey) => _searches[propertyKey];
        internal DataTransformer<TConfigured> GetPagging() => _pagging ?? new Pagging<TConfigured>(MaxPageNumber);

        protected void CreateSorting(string propertyKey,Expression<Func<TConfigured,object>> propertySelector)
        {
            var sorting = new Sorting<TConfigured>(propertySelector);

            _sortings.Add(propertyKey, sorting);
        }

        protected void CreateFilter(string propertyKey,Expression<Func<TConfigured, string>> propertySelector)
        {
            _filters.Add(propertyKey, new StringFilter<TConfigured>(propertySelector));
        }

        protected void CreateFilter(string propertyKey,Expression<Func<TConfigured, double>> propertySelector)
        {
            _filters.Add(propertyKey, new NumberFilter<TConfigured>(propertySelector));
        }

        protected void CreateFilter(string propertyKey, Expression<Func<TConfigured, IEnumerable<int>>> propertySelector)
        {
            _filters.Add(propertyKey, new CollectionFilter<TConfigured>(propertySelector));
        }

        protected void CreateSearch(string propertyKey, Expression<Func<TConfigured, string>> propertySelector)
        {
            _searches.Add(propertyKey, new Search<TConfigured>(propertySelector));
        }

        protected void CreatePagging(int maxPageNumberForConfigured)
        {
            if (maxPageNumberForConfigured > MaxPageNumber)
                maxPageNumberForConfigured = MaxPageNumber;

            _pagging = new Pagging<TConfigured>(maxPageNumberForConfigured);
        }


    }
}
