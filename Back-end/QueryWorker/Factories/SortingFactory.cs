using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Factories
{
    class SortingFactory : IQueryNodeFactory
    {
        public string[] SortedProperties { set; get; }
        public IQueryNode Create()
        {
            return new Sorting() { SortedProperties = SortedProperties};
        }
    }
}
