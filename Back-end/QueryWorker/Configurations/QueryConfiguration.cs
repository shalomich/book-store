using AutoMapper;
using QueryWorker.QueryNodeParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Configurations
{
    public abstract class QueryConfiguration
    {
        private readonly SelectorStorage _storage = new SelectorStorage();
        protected void CreateSorting<TClass,TProperty>(Expression<Func<TClass,TProperty>> propertySelector, string propertyName = null)
        {
            _storage.Add(propertyName, propertySelector);  
        }

        public Expression<Func<object,object>> GetSorting(string propertyName)
        {
            return _storage.Get(propertyName);
        } 
    }
}
