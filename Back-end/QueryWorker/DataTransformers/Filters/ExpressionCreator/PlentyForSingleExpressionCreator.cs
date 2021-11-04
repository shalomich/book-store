using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator
{
    internal class PlentyForSingleExpressionCreator<T> : IFilterExpressionCreator<T> where T : class
    {
        private const string Separator = ",";
        private Expression<Func<T, int>> PropertySelector { get; }

        public PlentyForSingleExpressionCreator(Expression<Func<T, int>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public Expression<Func<T, bool>> CreateFiltering(string filterValue)
        {
            Parse(filterValue, out IEnumerable<int> options);

            Expression<Func<int, bool>> comparer = value => options.Contains(value);

            return PropertySelector.Compose(comparer);
        }
        private void Parse(string filterValue, out IEnumerable<int> options)
        {
            var values = new HashSet<int>();

            foreach(string value in filterValue.Split(Separator))
            {
                if (int.TryParse(value, out int parsedValue))
                    values.Add(parsedValue);
            };

            options = values;
        }
    }
}
