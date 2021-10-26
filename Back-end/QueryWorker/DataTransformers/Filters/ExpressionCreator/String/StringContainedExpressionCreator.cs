using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator.String
{
    internal class StringContainedExpressionCreator<T> : StringExpressionCreator<T> where T : class
    {
        public StringContainedExpressionCreator(Expression<Func<T, string>> propertySelector) : base(propertySelector)
        {
        }

        protected override Expression<Func<string, bool>> CreateComparer(string comparedValue)
        {
            return value => comparedValue.Contains(value);
        }
    }
}