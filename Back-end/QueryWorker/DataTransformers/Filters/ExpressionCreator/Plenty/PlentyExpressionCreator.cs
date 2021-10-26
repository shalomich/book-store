using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator.Plenty
{
    internal class PlentyExpressionCreator<T> : IFilterExpressionCreator<T> where T : class
    {
        private Expression<Func<T, IEnumerable<int>>> PropertySelector { get; }

        public PlentyExpressionCreator(Expression<Func<T, IEnumerable<int>>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }
        public Expression<Func<T, bool>> CreateFiltering(string comparedValue)
        {
            var parsedValues = comparedValue
                        .Split(',', StringSplitOptions.None)
                        .Select(str => int.Parse(str));
                        
            Expression<Func<IEnumerable<int>,bool>> comparer = value => value.Intersect(parsedValues).Count() != 0;

            return PropertySelector.Compose(comparer);
        }
    }
}
