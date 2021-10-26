using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator.Number
{
    internal class NumberEqualOrLessExpressionCreator<T> : NumberExpressionCreator<T> where T : class
    {
        public NumberEqualOrLessExpressionCreator(Expression<Func<T, double>> propertySelector) : base(propertySelector)
        {
        }

        protected override Expression<Func<double, bool>> CreateComparer(double comparedValue)
        {
            return value => value <= comparedValue;
        }
    }
}
