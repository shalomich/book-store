using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Configurations
{
    public class SelectorStorage
    {
        private Dictionary<string, Expression<Func<object, object>>> _propertySelectors =
            new Dictionary<string, Expression<Func<object, object>>>();

        private void AddPropertySelector<TClass,TProperty>(string propertyName, Expression<Func<TClass, TProperty>> propertySelector)
        {
            Expression<Func<object, object>> selector = Expression.Lambda<Func<object, object>>(
                    Expression.Convert(propertySelector.Body, typeof(object)), 
                    propertySelector.Parameters);
            _propertySelectors.Add(propertyName, selector);
        }
        internal void Add<TClass,TProperty>(Expression<Func<TClass, TProperty>> propertySelector)
        {
            var member = propertySelector.Body as MemberExpression;
            if (member != null && member.Member is PropertyInfo property)
                AddPropertySelector(property.Name, propertySelector);
            else throw new ArgumentException();
        }

        internal void Add<TClass,TProperty>(string propertyName,Expression<Func<TClass, TProperty>> propertySelector)
        {
            if (propertyName == null)
                Add(propertySelector);
            else AddPropertySelector(propertyName.ToCapitalLetter(), propertySelector);
        }
        internal Expression<Func<object, object>> Get(string propertyName)
        {
            return _propertySelectors[propertyName];
        }
    }
}
