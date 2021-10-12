
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
        private Pagging<TConfigured> _pagging;

        private readonly Dictionary<string, DataTransformer<TConfigured>> _sortings = new Dictionary<string, DataTransformer<TConfigured>>();
        private readonly Dictionary<string, DataTransformer<TConfigured>> _filters = new Dictionary<string, DataTransformer<TConfigured>>();
        private readonly Dictionary<string, DataTransformer<TConfigured>> _searches = new Dictionary<string, DataTransformer<TConfigured>>();

        internal DataTransformer<TConfigured> GetSorting(string propertyKey) => _sortings[propertyKey];
        internal DataTransformer<TConfigured> GetFilter(string propertyKey) => _filters[propertyKey];
        internal DataTransformer<TConfigured> GetSearch(string propertyKey) => _searches[propertyKey];
        internal DataTransformer<TConfigured> GetPagging() => _pagging ?? new Pagging<TConfigured>();

        protected void CreateSorting(string propertyKey,Expression<Func<TConfigured,object>> propertySelector)
        {
            var sorting = new Sorting<TConfigured>(propertySelector);

            _sortings.Add(propertyKey.ToLowFirstLetter(), sorting);
        }

        protected void CreateFilter(string propertyKey,Expression<Func<TConfigured, string>> propertySelector)
        {
            _filters.Add(propertyKey.ToLowFirstLetter(), new StringFilter<TConfigured>(propertySelector));
        }

        protected void CreateFilter(string propertyKey,Expression<Func<TConfigured, double>> propertySelector)
        {
            _filters.Add(propertyKey.ToLowFirstLetter(), new NumberFilter<TConfigured>(propertySelector));
        }

        protected void CreateFilter(string propertyKey, Expression<Func<TConfigured, IEnumerable<int>>> propertySelector)
        {
            _filters.Add(propertyKey.ToLowFirstLetter(), new CollectionFilter<TConfigured>(propertySelector));
        }

        protected void CreateSearch(string propertyKey, Expression<Func<TConfigured, string>> propertySelector)
        {
            _searches.Add(propertyKey.ToLowFirstLetter(), new Search<TConfigured>(propertySelector));
        }

        protected void CreatePagging(int maxPageNumberForConfigured)
        {
            _pagging = new Pagging<TConfigured>(maxPageNumberForConfigured);
        }


    }
}
