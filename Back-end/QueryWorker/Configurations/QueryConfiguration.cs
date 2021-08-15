using AutoMapper;
using QueryWorker.QueryNodeParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Configurations
{
    public abstract class QueryConfiguration<TClass> where TClass : class
    {
        internal Dictionary<string, Sorting<TClass>> Sortings { get; }

        protected QueryConfiguration()
        {
            Sortings = new Dictionary<string, Sorting<TClass>>();
        }

        protected void CreateSorting<TProperty>(Expression<Func<TClass,TProperty>> propertySelector, string propertyName = null)
        {
            string sortingKey = GetPropertyName(propertyName, propertySelector);

            var sorting = new Sorting<TClass>(ToStoredSelector(propertySelector));

            Sortings.Add(sortingKey, sorting);
        }

        private Expression<Func<TClass, object>> ToStoredSelector<TProperty>(Expression<Func<TClass, TProperty>> propertySelector)
        {
            return Expression.Lambda<Func<TClass, object>>(
                    Expression.Convert(propertySelector.Body, typeof(object)),
                    propertySelector.Parameters);
        }
        private string GetPropertyName<TProperty>(string propertyName, Expression<Func<TClass, TProperty>> propertySelector)
        {
            if (propertyName == null)
            {
                var member = propertySelector.Body as MemberExpression;
                if (member != null && member.Member is PropertyInfo property)
                    return property.Name;
                else throw new ArgumentException();
            }

            return propertyName.ToCapitalLetter();
        }
    }
}
