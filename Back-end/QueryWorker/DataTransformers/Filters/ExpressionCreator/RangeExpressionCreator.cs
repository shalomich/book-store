using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator
{
    internal class RangeExpressionCreator<T> : IFilterExpressionCreator<T> where T : class
    {
        private Expression<Func<T, int>> PropertySelector { get; }
        public RangeExpressionCreator(Expression<Func<T, int>> propertySelector)
        {
            PropertySelector = propertySelector;
        }
        public Expression<Func<T, bool>> CreateFiltering(string filterValue)
        {
            Parse(filterValue, out int lowBound, out int highBound);

            Expression<Func<int, bool>> comparer = value =>
                value >= lowBound && value <= highBound;

            return PropertySelector.Compose(comparer);
        }

        private void Parse(string filterValue, out int lowBound, out int highBound)
        {
            var bounds = filterValue.Split("...")
                .Select(str => int.Parse(str)).ToArray();

            lowBound = bounds[0];
            highBound = bounds[1];
        }
    }
}
