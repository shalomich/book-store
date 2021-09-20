using QueryWorker.Extensions;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace QueryWorker.DataTransformers.Filters
{
    
    internal abstract class Filter<TClass,TProperty> : DataTransformer<TClass> where TClass : class
    {
        public Expression<Func<TClass, TProperty>> PropertySelector { init; get; }
        public TProperty ComparedValue { set; get; }
        public virtual FilterСomparison Comparison { set; get; } = FilterСomparison.Equal;

        protected Filter()
        {

        }
        protected Filter(Expression<Func<TClass, TProperty>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public override IQueryable<TClass> Transform(IQueryable<TClass> query)
        {           
            var comparer = ChooseComparer(ComparedValue, Comparison);
            
            Expression<Func<TClass, bool>> filterExpression = PropertySelector.Compose(comparer);

            return query.Where(filterExpression);
        }
        protected abstract Expression<Func<TProperty, bool>> ChooseComparer(TProperty comparedValue, FilterСomparison comparison);
    }
}
