using QueryWorker.Extensions;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace QueryWorker.DataTransformers.Filters
{
    
    internal abstract class Filter<TClass,TProperty> : IDataTransformer<TClass> where TClass : class
    {
        private readonly Expression<Func<TClass, TProperty>> _propertySelector;
        public TProperty ComparedValue { set; get; }
        public virtual FilterСomparison Comparison { set; get; } = FilterСomparison.Equal;

        protected Filter(Expression<Func<TClass, TProperty>> propertySelector)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        protected Filter(Expression<Func<TClass, TProperty>> propertySelector, TProperty comparedValue, FilterСomparison comparison = FilterСomparison.Equal) : this(propertySelector)
        {
            ComparedValue = comparedValue;
            Comparison = comparison;
        }

        public IQueryable<TClass> Transform(IQueryable<TClass> query)
        {           
            var comparer = ChooseComparer(ComparedValue, Comparison);
            
            Expression<Func<TClass, bool>> filterExpression = _propertySelector.Compose(comparer);

            return query.Where(filterExpression);
        }
        protected abstract Expression<Func<TProperty, bool>> ChooseComparer(TProperty comparedValue, FilterСomparison comparison);
    }
}
