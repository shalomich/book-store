using Microsoft.Extensions.Configuration;
using QueryWorker.QueryNodes;
using QueryWorker.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryWorker.Factories;
using System.Reflection;
using QueryWorker.Parsers;

namespace QueryWorker
{
    public class QueryTransformer
    {
        private FilterFactory _filterFactory;
        private SortingFactory _sortingFactory;
        private PaggingFactory _paggingFactory;
        public QueryTransformer(Assembly assembly)
        {
            var configurationFinder = new ConfigurationFinder(assembly);
            _sortingFactory = new SortingFactory(configurationFinder);
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryParams parameters) where T : class
        {
            var queries = new Queue<IQueryNode<T>>();
            
            //foreach (string filterQuery in parameters.Filter)
              //  queries.Enqueue();
            
            foreach (string sortingQuery in parameters.Sorting)
                queries.Enqueue(_sortingFactory.Create<T>(sortingQuery));

            //queries.Enqueue(CreateNode(_paggingFactory, parameters.Pagging));

            while (queries.TryDequeue(out IQueryNode<T> node) != false)
                data = node.Execute(data);
            
            return data;
        }
    }
}
