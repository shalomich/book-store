using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator.String
{
    internal abstract class StringExpressionCreator<T> : IFilterExpressionCreator<T> where T : class
    {
        private Expression<Func<T, string>> PropertySelector { get; }
        protected StringExpressionCreator(Expression<Func<T, string>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        protected abstract Expression<Func<string, bool>> CreateComparer(string comparedValue);
        public Expression<Func<T, bool>> CreateFiltering(string comparedValue)
        {
            return PropertySelector.Compose(CreateComparer(comparedValue));
        }
    }
}
