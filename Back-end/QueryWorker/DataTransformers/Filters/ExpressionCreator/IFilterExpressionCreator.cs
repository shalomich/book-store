using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator
{
    internal interface IFilterExpressionCreator<T> where T : class
    {
        public Expression<Func<T, bool>> CreateFiltering(string comparedValue);
    }
}
