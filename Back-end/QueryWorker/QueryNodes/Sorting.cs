using QueryWorker.QueryNodes;
using QueryWorker.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Various;

namespace QueryWorker
{
    public class Sorting : IQueryNode, IParsed
    {
        private static readonly string _notNullPropertyNameMessage;

        private string _notExistPropertyNameMessage;
        private string _notExistSortedPropertyMessage;
        
        public event Action<string, string, IInformed> Accepted;
        public event Action<string, string, IInformed> Crashed;

        public string PropertyName { set; get; }
        public bool IsAscending { set; get; } = true;
        public string[] SortedProperties { set; get; }

        static Sorting()
        {
            _notNullPropertyNameMessage = ExceptionMessages
               .GetMessage(ExceptionMessageType.Null, nameof(PropertyName), "name");

        }
        public Sorting()
        {
        }

        public Sorting(string propertyName, bool isAscending)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            IsAscending = isAscending;
        }

        public IQueryable<T> Execute<T>(IQueryable<T> query)
        {
            if (isValidSorting() == false)
                return query;

            var property = typeof(T).GetProperty(PropertyName);

            if (property == null)
            {
                IEnumerable<string> propertyNames = typeof(T).GetProperties().Select(property => property.Name);
                propertyNames = propertyNames.Intersect(SortedProperties);
                _notExistPropertyNameMessage = ExceptionMessages
                    .GetMessage(ExceptionMessageType.NotExist, nameof(PropertyName), propertyNames.Aggregate((str1, str2) => $"{str1} {str2}"));

                Crashed?.Invoke(nameof(PropertyName), _notExistPropertyNameMessage, this);
                return query;
            }
            
            Expression<Func<T, object>> valueGetting = obj => property.GetValue(obj);

            query = IsAscending == true ? query.AppendOrderBy(valueGetting) : query.AppendOrderByDescending(valueGetting);

            return query;
        }

        private bool isValidSorting()
        {
            bool isValid = true;

            if (PropertyName == null)
            {
                Crashed?.Invoke(nameof(PropertyName), _notNullPropertyNameMessage, this);
                isValid = false;
            }
            else if (SortedProperties?.Contains(PropertyName) == false)
            {
                _notExistSortedPropertyMessage = ExceptionMessages
                    .GetMessage(ExceptionMessageType.NotExist, nameof(SortedProperties), SortedProperties.Aggregate((str1, str2) => $"{str1} {str2}"));
                Crashed?.Invoke(nameof(PropertyName), _notExistSortedPropertyMessage, this);
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
            return $"Sorting(propertyName: {PropertyName})";
        }
    }
}
