using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Factories
{
    class FilterFactory : IQueryNodeFactory
    {
        public string[] FilteredProperties { set; get; }
        public IQueryNode Create()
        {
            return new Filter() { FilteredProperties = FilteredProperties};
        }
    }
}
