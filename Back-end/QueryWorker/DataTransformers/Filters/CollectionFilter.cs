using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters
{
    internal class CollectionFilter<T> : Filter<T, IEnumerable<string>> where T : class
    {
        private FilterСomparison _comparison = FilterСomparison.Equal;
        public CollectionFilter(Expression<Func<T, IEnumerable<string>>> propertySelector) : base(propertySelector)
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
        protected override Expression<Func<IEnumerable<string>, bool>> ChooseComparer(IEnumerable<string> comparedValue, FilterСomparison comparison)
        {
            return value => value.Any(str => comparedValue.Contains(str));
        }
    }
}
