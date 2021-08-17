
using QueryWorker.Args;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Parts
{
    internal abstract class QueryPart
    {
        protected QueryPart _nextPart;

        public QueryPart SetNextPart(QueryPart nexPart)
        {
            _nextPart = nexPart;

            return this;
        }
        public abstract IQueryable<T> Change<T>(IQueryable<T> data, QueryArgs args, 
            QueryConfiguration<T> config) where T : class;
        
    }
}
