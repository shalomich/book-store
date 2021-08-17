
using QueryWorker.Extensions;
using QueryWorker.QueryNodes;
using QueryWorker.QueryNodes.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace QueryWorker.Configurations
{
    public abstract class QueryConfiguration<TClass> where TClass : class
    {
        internal Dictionary<string, Sorting<TClass>> Sortings { get; }
        internal Dictionary<string, StringFilter<TClass>> StringFilters { get; }
        internal Dictionary<string, NumberFilter<TClass>> NumberFilters { get; }
        internal Dictionary<string, CollectionFilter<TClass>> CollectionFilters { get; }
        protected QueryConfiguration()
        {
            Sortings = new Dictionary<string, Sorting<TClass>>();
            StringFilters = new Dictionary<string, StringFilter<TClass>>();
            NumberFilters = new Dictionary<string, NumberFilter<TClass>>();
            CollectionFilters = new Dictionary<string, CollectionFilter<TClass>>();
        }

        protected void CreateSorting<TProperty>(string propertyKey,Expression<Func<TClass,TProperty>> propertySelector)
        {
            var sorting = new Sorting<TClass>(propertySelector.ConvertBody<TClass, TProperty, object>());

            Sortings.Add(propertyKey, sorting);
        }

        protected void CreateFilter(string propertyKey,Expression<Func<TClass, string>> propertySelector)
        {
            var filter = new StringFilter<TClass>(propertySelector);

            StringFilters.Add(propertyKey, filter);
        }

        protected void CreateFilter(string propertyKey,Expression<Func<TClass, double>> propertySelector)
        {
            var filter = new NumberFilter<TClass>(propertySelector);

            NumberFilters.Add(propertyKey, filter);
        }

        protected void CreateFilter(string propertyKey, Expression<Func<TClass, IEnumerable<string>>> propertySelector)
        {
            var filter = new CollectionFilter<TClass>(propertySelector);

            CollectionFilters.Add(propertyKey, filter);
        }
    }
}
