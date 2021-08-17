using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.QueryNodes.Filters
{
    internal class NumberFilter<T> : Filter<T,double> where T : class
    {
        public NumberFilter(Expression<Func<T, double>> propertySelector) : base(propertySelector)
        {
        }

        protected override Expression<Func<double, bool>> ChooseComparer(double comparedValue, FilterСomparison comparison)
            => comparison switch
        {
            FilterСomparison.Equal => value => value == comparedValue,
            FilterСomparison.More => value => value > comparedValue,
            FilterСomparison.Less => value => value < comparedValue,
            FilterСomparison.EqualOrMore => value => value >= comparedValue,
            FilterСomparison.EqualOrLess => value => value <= comparedValue
        };

        protected override double Parse(string comparedValue)
        {
            return double.Parse(comparedValue);
        }
    }
}
