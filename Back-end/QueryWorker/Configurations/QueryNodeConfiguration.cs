using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Configurations
{
    public abstract class QueryNodeConfiguration<TClass> where TClass : class
    {
        private Dictionary<string, Expression<Func<TClass, object>>> _propertySelectors =
            new Dictionary<string, Expression<Func<TClass, object>>>();

        private protected void AddPropertySelector<TProperty>(Expression<Func<TClass, TProperty>> propertySelector)
        {
            var member = propertySelector.Body as MemberExpression;
            if (member != null && member.Member is PropertyInfo property)
            {
                Expression<Func<TClass, object>> selector = Expression.Lambda<Func<TClass, object>>(
                    Expression.Convert(propertySelector.Body, typeof(object)), propertySelector.Parameters);
                _propertySelectors.Add(property.Name, selector);
            }
            else throw new ArgumentException();
        }
        internal Expression<Func<TClass, object>> GetPropertySelector(string propertyName)
        {
            return _propertySelectors[propertyName];
        }
    }
}
