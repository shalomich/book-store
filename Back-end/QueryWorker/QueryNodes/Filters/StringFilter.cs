using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.QueryNodes.Filters
{
    internal class StringFilter<T> : Filter<T, string> where T : class
    {
        public StringFilter(Expression<Func<T, string>> propertySelector) : base(propertySelector)
        {
        }

        protected override Expression<Func<string, bool>> ChooseComparer(string comparedValue, FilterСomparison comparison)
        {
            if (comparison == FilterСomparison.Equal)
                return value => value == comparedValue;

            throw new InvalidOperationException();
        }

        protected override string Parse(string comparedValue)
        {
            return comparedValue;
        }
    }
}
