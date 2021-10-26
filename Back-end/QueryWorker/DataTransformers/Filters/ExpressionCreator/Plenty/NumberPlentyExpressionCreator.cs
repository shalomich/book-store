using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator.Plenty
{
    internal class NumberPlentyExpressionCreator<T> : IFilterExpressionCreator<T> where T : class
    {
        private Expression<Func<T, int>> PropertySelector { get; }

        public NumberPlentyExpressionCreator(Expression<Func<T, int>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }
        public Expression<Func<T, bool>> CreateFiltering(string comparedValue)
        {
            var parsedValues = comparedValue
                        .Split(',', StringSplitOptions.None)
                        .Select(str => int.Parse(str));

            Expression<Func<int, bool>> comparer = value => parsedValues.Contains(value);

            return PropertySelector.Compose(comparer);
        }
    }
}
