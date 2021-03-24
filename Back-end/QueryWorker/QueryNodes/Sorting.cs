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
            var property = typeof(T).GetProperty(PropertyName);

            if (property == null)
                throw new ArgumentException(String.Format(_notExistPropertyNameMessage, PropertyName));

            if (SortedProperties?.Contains(PropertyName) == false)
                throw new ArgumentException();

            Expression<Func<T, object>> valueGetting = obj => property.GetValue(obj);

            query = IsAscending == true ? query.AppendOrderBy(valueGetting) : query.AppendOrderByDescending(valueGetting);

            return query;
        }

        public void Accept(IQueryParser parser)
        {
            parser.Parse(this);
        }
    }
}
