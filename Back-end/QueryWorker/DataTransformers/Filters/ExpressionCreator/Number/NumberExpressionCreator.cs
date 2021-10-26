using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator.Number
{
    internal abstract class NumberExpressionCreator<T> : IFilterExpressionCreator<T> where T : class
    {
        private Expression<Func<T, double>> PropertySelector { get; }

        public NumberExpressionCreator(Expression<Func<T, double>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        protected abstract Expression<Func<double, bool>> CreateComparer(double comparedValue);
        public Expression<Func<T, bool>> CreateFiltering(string comparedValue)
        {
            double parsedValue = double.Parse(comparedValue);

            return PropertySelector.Compose(CreateComparer(parsedValue));
        }
    }
}
