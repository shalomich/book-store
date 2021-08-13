using QueryWorker.Parsers;
using QueryWorker.QueryNodeParams;
using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Factories
{
    internal abstract class QueryNodeFactory
    {
        protected abstract IQueryParser Parser { get; }
        protected IQueryNodeParams GetParams(string query) => Parser.Parse(query);
        public abstract IQueryNode<T> Create<T>(string query) where T : class;
    }
}
