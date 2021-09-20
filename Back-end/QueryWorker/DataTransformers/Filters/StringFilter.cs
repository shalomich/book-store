using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters
{
    internal class StringFilter<T> : Filter<T, string> where T : class
    {
        private FilterСomparison _comparison = FilterСomparison.Equal;

        public StringFilter() : base()
        {

        }
        public StringFilter(Expression<Func<T, string>> propertySelector) : base(propertySelector)
        {
        }

        public override FilterСomparison Comparison
        {
            set
            {
                if (value == FilterСomparison.More || value == FilterСomparison.Less)
                    throw new ArgumentException();
                _comparison = value;
            }
            get
            {
                return _comparison;
            }
        }

        protected override Expression<Func<string, bool>> ChooseComparer(string comparedValue, FilterСomparison comparison)
            => comparison switch
            {
                FilterСomparison.Equal => value => value == comparedValue,
                FilterСomparison.EqualOrMore => value => value.Contains(comparedValue),
                FilterСomparison.EqualOrLess => value => comparedValue.Contains(value)
            };
    }
}
