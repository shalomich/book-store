using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters
{
    internal class CollectionFilter<T> : Filter<T, IEnumerable<int>> where T : class
    {
        private FilterСomparison _comparison = FilterСomparison.Equal;

        public CollectionFilter() : base()
        {
        }

        public CollectionFilter(Expression<Func<T, IEnumerable<int>>> propertySelector) : base(propertySelector)
        {
        }

        public override FilterСomparison Comparison
        {
            set
            {
                if (value != FilterСomparison.Equal)
                    throw new ArgumentException();
                _comparison = value;
            }
            get
            {
                return _comparison;
            }
        }
        protected override Expression<Func<IEnumerable<int>, bool>> ChooseComparer(IEnumerable<int> comparedValue, FilterСomparison comparison)
        {
            return value => value.Any(number => comparedValue.Contains(number));
        }
    }
}
