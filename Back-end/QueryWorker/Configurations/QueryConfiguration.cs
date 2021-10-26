using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using QueryWorker.DataTransformers.Filters.ExpressionCreator.Number;
using QueryWorker.DataTransformers.Filters.ExpressionCreator.String;
using QueryWorker.DataTransformers.Filters.ExpressionCreator.Plenty;
using QueryWorker.Extensions;

namespace QueryWorker.Configurations
{
    public abstract class QueryConfiguration<T> where T : class
    {
        private readonly Dictionary<string, Sorting<T>> _sortings = new Dictionary<string, Sorting<T>>();
        private readonly Dictionary<string, Search<T>> _searches = new Dictionary<string, Search<T>>();
        private readonly Dictionary<(string, FilterСomparison), Filter<T>> _filters = new Dictionary<(string, FilterСomparison), Filter<T>>();

        internal Sorting<T> GetSorting(string propertyName) => _sortings[propertyName];
        internal Filter<T> GetFilter(string propertyName, FilterСomparison сomparison) 
            => _filters[(propertyName, сomparison)];
        internal Search<T> GetSearch(string propertyName) => _searches[propertyName];
        
        protected void CreateSorting(string propertyName,Expression<Func<T,object>> propertySelector)
        {
            var sorting = new Sorting<T>(propertySelector);

            _sortings.Add(propertyName.ToLowFirstLetter(), sorting);
        }

        protected void CreateRangeFilter(string propertyName,Expression<Func<T, string>> propertySelector)
        {
            propertyName = propertyName.ToLowFirstLetter();

            _filters.Add(key: (propertyName, FilterСomparison.Equal),
                value: new Filter<T>(new StringEqualExpressionCreator<T>(propertySelector)));
            
            _filters.Add(key: (propertyName, FilterСomparison.EqualOrMore),
                value: new Filter<T>(new StringContainsExpressionCreator<T>(propertySelector)));
            
            _filters.Add(key: (propertyName, FilterСomparison.EqualOrLess),
                value: new Filter<T>(new StringContainedExpressionCreator<T>(propertySelector)));
        }

        protected void CreateRangeFilter(string propertyName,Expression<Func<T, double>> propertySelector)
        {
            propertyName = propertyName.ToLowFirstLetter();

            _filters.Add(key: (propertyName, FilterСomparison.EqualOrLess), 
                value: new Filter<T>(new NumberEqualOrLessExpressionCreator<T>(propertySelector)));

            _filters.Add(key: (propertyName, FilterСomparison.EqualOrMore),
                value: new Filter<T>(new NumberEqualOrMoreExpressionCreator<T>(propertySelector)));
        }

        protected void CreatePlentyFilter(string propertyName, Expression<Func<T, IEnumerable<int>>> propertySelector)
        {
            _filters.Add(key: (propertyName.ToLowFirstLetter(), FilterСomparison.Equal),
                value: new Filter<T>(new PlentyExpressionCreator<T>(propertySelector)));
        }

        protected void CreatePlentyFilter(string propertyName, Expression<Func<T, int>> propertySelector)
        {
            _filters.Add(key: (propertyName.ToLowFirstLetter(), FilterСomparison.Equal),
                value: new Filter<T>(new NumberPlentyExpressionCreator<T>(propertySelector)));
        }
        protected void CreateSearch(string propertyName, Expression<Func<T, string>> propertySelector)
        {
            _searches.Add(propertyName.ToLowFirstLetter(), new Search<T>(propertySelector));
        }
    }
}
