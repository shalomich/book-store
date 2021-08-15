using QueryWorker.Parsers;
using QueryWorker.QueryNodeParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker
{
    abstract class QueryPart
    {
        protected readonly ConfigurationFinder _configurationFinder;
        protected QueryPart _nextPart;

        protected QueryPart(ConfigurationFinder configurationFinder)
        {
            _configurationFinder = configurationFinder ?? throw new ArgumentNullException(nameof(configurationFinder));
        }

        public void SetNextPart(QueryPart nexPart)
        {
            _nextPart = nexPart;
        }
        public abstract IQueryable<T> Change<T>(IQueryable<T> data, QueryArgs args) where T : class;
        
    }
}
