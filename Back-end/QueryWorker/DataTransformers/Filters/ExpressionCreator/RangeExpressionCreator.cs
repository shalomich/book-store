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
        private const string Separator = "...";
        private const int DefaultLowBound = int.MinValue;
        private const int DefaultHighBound = int.MaxValue;
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
            if (filterValue.Contains(Separator) == false)
            {
                lowBound = DefaultLowBound;
                highBound = DefaultHighBound;
                return;
            }

            var bounds = filterValue.Split(Separator).ToArray();
            if (int.TryParse(bounds[0], out lowBound) == false)
                lowBound = DefaultLowBound;

            if (int.TryParse(bounds[1], out highBound) == false)
                highBound = DefaultHighBound;
        }
    }
}
