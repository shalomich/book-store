using QueryWorker.QueryNodes;
using QueryWorker.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Various;

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
        
        private static readonly string _notNullPropertyNameMessage;
        private static readonly string _notNullValueMessage;
        private static readonly string _notNullFilterComparisonMessage;

        private string _notExistFilteredPropertyMessage;
        private string _notExistPropertyNameMessage;
        private string _invalidValueTypeMessage;

        public event Action<string, string, IInformed> Accepted;
        public event Action<string, string, IInformed> Crashed;

        public string PropertyName { set;get;}
        public IComparable Value { set; get; }
        public FilterСomparison? FilterСomparisonValue { set; get; }
        public string[] FilteredProperties { set; get; }

        static Filter()
        {
            _notNullPropertyNameMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.Null, nameof(PropertyName), "name");
            _notNullValueMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.Null, nameof(Value), "1 or 'abc'");
            _notNullFilterComparisonMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.Null, nameof(FilterСomparisonValue), FilterСomparison.Equal.ToString());
        }
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
                IEnumerable<string> propertyNames = typeof(T).GetProperties().Select(property => property.Name);
                propertyNames = propertyNames.Intersect(FilteredProperties);
                _notExistPropertyNameMessage = ExceptionMessages
                    .GetMessage(ExceptionMessageType.NotExist,nameof(PropertyName), propertyNames.Aggregate((str1, str2) => $"{str1} {str2}"));
                
                Crashed?.Invoke(nameof(PropertyName), _notExistPropertyNameMessage,this);
                return query;
            }
               
            if (property.PropertyType != Value.GetType())
            {
                _invalidValueTypeMessage = ExceptionMessages
                    .GetMessage(ExceptionMessageType.Invalid, nameof(Value), Value.GetType().Name);
                Crashed?.Invoke(nameof(Value), _invalidValueTypeMessage,this);
                return query;
            }
            
            Func<T, bool> fieldComparison = obj => ((IComparable)property.GetValue(obj)).CompareTo(Value) == (int) FilterСomparisonValue;
            
            query = query.Where(fieldComparison).AsQueryable();
            
            return query;
        }

        private bool isValidFilter()
        {
            bool isValid = true;

            if (PropertyName == null)
            {
                Crashed?.Invoke(nameof(PropertyName), _notNullPropertyNameMessage,this);
                isValid = false;
            }
            else if (FilteredProperties?.Contains(PropertyName) == false)
            {
                _notExistFilteredPropertyMessage = ExceptionMessages
                    .GetMessage(ExceptionMessageType.NotExist, nameof(FilteredProperties), FilteredProperties.Aggregate((str1, str2) => $"{str1} {str2}"));
                Crashed?.Invoke(nameof(PropertyName), _notExistFilteredPropertyMessage,this);
                isValid = false;
            }

            if (Value == null)
            {
                Crashed?.Invoke(nameof(Value), _notNullValueMessage,this);
                isValid = false;
            }

            if (FilterСomparisonValue == null)
            {
                Crashed?.Invoke(nameof(FilterСomparisonValue), _notNullFilterComparisonMessage,this);
                isValid = false;
            }

            return isValid;

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
