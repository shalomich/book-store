using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.QueryNodes.Filters
{
    internal class CollectionFilter<T> : Filter<T, IEnumerable<string>> where T : class
    {
        public CollectionFilter(Expression<Func<T, IEnumerable<string>>> propertySelector) : base(propertySelector)
        {
        }

        protected override Expression<Func<IEnumerable<string>, bool>> ChooseComparer(IEnumerable<string> comparedValue, FilterСomparison comparison)
        {

            if (comparison == FilterСomparison.Equal)
                return value => value.Any(str => comparedValue.Contains(str));

            throw new InvalidOperationException();
        }

        protected override ICollection<string> Parse(string comparedValue)
        {
            return comparedValue.Split(',');
        }
    }
}
