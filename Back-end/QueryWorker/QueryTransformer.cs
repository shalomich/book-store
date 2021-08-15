using Microsoft.Extensions.Configuration;
using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using QueryWorker.Parsers;
using AutoMapper;
using QueryWorker.QueryNodeParams;
using QueryWorker.Configurations;

namespace QueryWorker
{
    public class QueryTransformer
    {
        private readonly ConfigurationFinder _configurationFinder;

        public QueryTransformer(Assembly assembly)
        {
            _configurationFinder = new ConfigurationFinder(assembly);
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryParams parameters) where T : class
        {
            var queries = new Queue<IQueryNode>();
            foreach (string sortingQuery in parameters.Sorting)
                queries.Enqueue(new Sorting(new SortingParser().Parse(sortingQuery) as SortingArgs));

            var queryConfig =  _configurationFinder.Find();
            while (queries.TryDequeue(out IQueryNode node) != false)
                data = node.Execute(data, queryConfig);
            
            return data;
        }
    }
}
