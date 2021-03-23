using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Factories
{
    class PaggingFactory : IQueryNodeFactory
    {
        public int MaxPageSize { set; get; }
        public IQueryNode Create()
        {
            return new Pagging() { MaxPageSize = MaxPageSize};
        }
    }
}
