using QueryWorker.Extensions;
using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace QueryWorker.QueryNodes.Filters
{
    
    public enum FilterСomparison
    {
        Equal = 0,
        More = 1,
        Less = 2,
        EqualOrMore = 3,
        EqualOrLess = 4
    }
    internal abstract class Filter<TClass,TProperty> : IQueryNode<TClass> where TClass : class
    {
        private readonly Expression<Func<TClass, TProperty>> _propertySelector;
        public string ComparedValue { set; get; }
        public FilterСomparison Comparison { set; get; } = FilterСomparison.Equal;

        public Filter(Expression<Func<TClass, TProperty>> propertySelector)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public IQueryable<TClass> Execute(IQueryable<TClass> query)
        {
            TProperty convertedComparedValue = Parse(ComparedValue);
            
            var comparer = ChooseComparer(convertedComparedValue, Comparison);
            
            Expression<Func<TClass, bool>> filterExpression = _propertySelector.Compose(comparer);

            return query.Where(filterExpression);
        }

        protected abstract TProperty Parse(string comparedValue);
        protected abstract Expression<Func<TProperty, bool>> ChooseComparer(TProperty comparedValue, FilterСomparison comparison);
    }
}
