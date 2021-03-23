using QueryWorker.QueryNodes;
using QueryWorker.Visitors;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryWorker
{
    public enum FilterСomparison
    {
        More = 1,
        Equal = 0,
        Less = -1
    }
    public class Filter : IQueryNode, IParsed
    {
        private const string _notExistPropertyNameMessage = "This class does not exist property named {0}";
        private const string _invalidValueTypeMessage = "This property has not type named {0}";
        public string PropertyName { set;get;}
        public IComparable Value { set; get; }
        public FilterСomparison FilterСomparisonValue { set; get; }

        public string[] FilteredProperties { set; get; }

        public Filter()
        {

        }

        public Filter(string propertyName, IComparable value, FilterСomparison сomparisonValue)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            FilterСomparisonValue = сomparisonValue;
        }

        public IQueryable<T> Execute<T>(IQueryable<T> query)
        {
            var property = typeof(T).GetProperty(PropertyName);

            if (property == null)
                throw new ArgumentException(String.Format(_notExistPropertyNameMessage,PropertyName));

            if (FilteredProperties?.Contains(PropertyName) == false)
                throw new ArgumentException();

            if (property.PropertyType != Value.GetType())
                throw new ArgumentException(String.Format(_invalidValueTypeMessage, Value.GetType()));

            Func<T, bool> fieldComparison = obj => ((IComparable)property.GetValue(obj)).CompareTo(Value) == (int) FilterСomparisonValue;
            
            query = query.Where(fieldComparison).AsQueryable();
            
            return query;
        }

        public void Accept(IQueryParser parser)
        {
            parser.Parse(this);
        }
    }
}
