using QueryWorker.Parsers;
using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Factories
{
    class PaggingFactory : QueryNodeFactory
    {
        public int MaxPageSize { set; get; }

        protected override IQueryParser Parser => throw new NotImplementedException();

        public override IQueryNode<T> Create<T>(string query)
        {
            throw new NotImplementedException();
        }
    }
}
