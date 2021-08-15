using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace QueryWorker
{
    /*
    public enum FilterСomparison
    {
        More = 1,
        Equal = 0,
        Less = -1
    }
    internal class Filter<T> : IQueryNode<T> where T : class
    {     
        public FilterСomparison FilterСomparisonValue { set; get; }
        public Predicate<T> PropertyPredicate { set; get; }

        public Filter(Predicate<T> propertyPredicate, FilterСomparison filterСomparisonValue)
        {
            FilterСomparisonValue = filterСomparisonValue;
            PropertyPredicate = propertyPredicate ?? throw new ArgumentNullException(nameof(propertyPredicate));
        }

        public IQueryable<T> Execute(IQueryable<T> query)
        {
            var property = typeof(T).GetProperty(PropertyName);
            
            Func<T, bool> fieldComparison = obj => ((IComparable)property.GetValue(obj)).CompareTo(Value) == (int) FilterСomparisonValue;
            
            query = query.Where(fieldComparison).AsQueryable();
            
            return query;
        }
    }*/
}
