using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator
{
    internal class PlentyForCollectionExpressionCreator<T> : IFilterExpressionCreator<T> where T : class
    {
        private Expression<Func<T, IEnumerable<int>>> PropertySelector { get; }

        public PlentyForCollectionExpressionCreator(Expression<Func<T, IEnumerable<int>>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public Expression<Func<T, bool>> CreateFiltering(string filterValue)
        {
            Parse(filterValue, out int[] options);

            var comparer = CreateComparer(options[0]);

            for (int i = 1; i < options.Length; i++)
                comparer = comparer.And(CreateComparer(options[i]));

            return PropertySelector.Compose(comparer);
        }

        private Expression<Func<IEnumerable<int>, bool>> CreateComparer(int option)
        {
            return value => value.Contains(option);
        }

        private void Parse(string filterValue, out int[] options)
        {
            options = filterValue.Split(',')
                .Select(str => int.Parse(str))
                .ToArray();
        }
    }
}
