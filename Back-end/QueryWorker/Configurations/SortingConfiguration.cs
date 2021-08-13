using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Configurations
{
    public abstract class SortingConfiguration<TClass> : QueryNodeConfiguration<TClass> where TClass : class
    {
        protected void CreateSorting<TProperty>(Expression<Func<TClass,TProperty>> propertySelector)
        {
            AddPropertySelector(propertySelector);
        }
    }
}
