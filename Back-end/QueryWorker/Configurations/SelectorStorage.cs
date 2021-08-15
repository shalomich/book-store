using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Configurations
{
    public class SelectorStorage<TClass> where TClass : class
    {
        private Dictionary<string, Expression<Func<TClass, object>>> _propertySelectors =
            new Dictionary<string, Expression<Func<TClass, object>>>();

        private void AddPropertySelector<TProperty>(string propertyName, Expression<Func<TClass, TProperty>> propertySelector)
        {
            Expression<Func<TClass, object>> selector = Expression.Lambda<Func<TClass, object>>(
                    Expression.Convert(propertySelector.Body, typeof(object)), 
                    propertySelector.Parameters);
            _propertySelectors.Add(propertyName, selector);
        }
        internal void Add<TProperty>(Expression<Func<TClass, TProperty>> propertySelector)
        {
            var member = propertySelector.Body as MemberExpression;
            if (member != null && member.Member is PropertyInfo property)
                AddPropertySelector(property.Name, propertySelector);
            else throw new ArgumentException();
        }

        internal void Add<TProperty>(string propertyName,Expression<Func<TClass, TProperty>> propertySelector)
        {
            if (propertyName == null)
                Add(propertySelector);
            else AddPropertySelector(propertyName.ToCapitalLetter(), propertySelector);
        }
        internal Expression<Func<TClass, object>> Get(string propertyName)
        {
            return _propertySelectors[propertyName];
        }
    }
}
