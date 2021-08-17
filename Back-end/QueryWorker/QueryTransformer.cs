
using System.Linq;
using System.Reflection;
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.Parts;

namespace QueryWorker
{
    public class QueryTransformer
    {
        private readonly QueryPart _queryPart;
        
        private readonly ConfigurationFinder _configurationFinder;
        public QueryTransformer(Assembly assembly)
        {
            _configurationFinder = new ConfigurationFinder(assembly);

            _queryPart = new FilterPart().SetNextPart(new SortingPart());
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryArgs args) where T : class
        {
            var config = _configurationFinder.Find<T>();
            
            return _queryPart.Change(data, args, config);
        }
    }
}
