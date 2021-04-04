using QueryWorker.QueryNodes;
using QueryWorker.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace QueryWorker
{
    public class Sorting : IQueryNode, IParsed
    {
        private const string _notExistPropertyNameMessage = "This class does not exist property named {0}";

        public event Action<string, string> Accepted;
        public event Action<string, string> Crashed;

        public string PropertyName { set; get; }
        public bool IsAscending { set; get; } = true;
        public string[] SortedProperties { set; get; }

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
                string error = String.Format(_notExistPropertyNameMessage, PropertyName);
                Crashed?.Invoke(nameof(PropertyName), $"{error}, {this.ToString()}");
                return query;
            }
            
            Expression<Func<T, object>> valueGetting = obj => property.GetValue(obj);

            query = IsAscending == true ? query.AppendOrderBy(valueGetting) : query.AppendOrderByDescending(valueGetting);

            return query;
        }

        private bool isValidSorting()
        {
            if (PropertyName == null)
            {
                string error = "propertyName can not be null";
                Crashed?.Invoke(nameof(PropertyName), $"{error}, {this.ToString()}");
            }
            else if (SortedProperties?.Contains(PropertyName) == false)
            {
                string error = $"Sorted properties don't contain this property ({PropertyName})";
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
            return $"Sorting(propertyName: {PropertyName})";
        }
    }
}
