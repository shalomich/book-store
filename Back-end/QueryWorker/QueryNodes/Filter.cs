using QueryWorker.QueryNodes;
using QueryWorker.Visitors;
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

        public event Action<string, string> Accepted;
        public event Action<string, string> Crashed;

        public string PropertyName { set;get;}
        public IComparable Value { set; get; }
        public FilterСomparison? FilterСomparisonValue { set; get; }

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
            if (isValidFilter() == false)
                return query;

            var property = typeof(T).GetProperty(PropertyName);

            if (property == null)
            {
                string error = String.Format(_notExistPropertyNameMessage, PropertyName);
                Crashed?.Invoke(nameof(PropertyName), $"{error}, {this.ToString()}");
                return query;
            }
               
            if (property.PropertyType != Value.GetType())
            {
                string error = String.Format(_invalidValueTypeMessage, Value.GetType());
                Crashed?.Invoke(nameof(PropertyName), $"{error}, {this.ToString()}");
                return query;
            }
            
            Func<T, bool> fieldComparison = obj => ((IComparable)property.GetValue(obj)).CompareTo(Value) == (int) FilterСomparisonValue;
            
            query = query.Where(fieldComparison).AsQueryable();
            
            return query;
        }

        private bool isValidFilter()
        {
            if (PropertyName == null)
            {
                string error = "propertyName can not be null";
                Crashed?.Invoke(nameof(PropertyName), $"{error}, {this.ToString()}");
            }
            else if (Value == null)
            {
                string error = "value can not be null";
                Crashed?.Invoke(nameof(PropertyName), $"{error}, {this.ToString()}");
            }
            else if (FilterСomparisonValue == null)
            {
                string error = "filterComparisonValue can not be null";
                Crashed?.Invoke(nameof(PropertyName), $"{error}, {this.ToString()}");
            }
            else if (FilteredProperties?.Contains(PropertyName) == false)
            {
                string error = $"Filtered properties don't contain this property ({PropertyName})";
                Crashed?.Invoke(nameof(PropertyName), $"{error}, {this.ToString()}");
            }
            else return true;

            return false;
        }

        public void Accept(IQueryParser parser)
        {
            parser.Parse(this);
        }

        public override string ToString()
        {
            return $"Filter(propertyName: {PropertyName}, value: {Value}, filterСomparisonValue: {FilterСomparisonValue.ToString()} )";
        }
    }
}
