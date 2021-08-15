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
using QueryWorker.Parts;

namespace QueryWorker
{
    public class QueryTransformer
    {
        private readonly QueryPart _queryPart;

        public QueryTransformer(Assembly assembly)
        {
            var configurationFinder = new ConfigurationFinder(assembly);

            _queryPart = new SortingPart(configurationFinder);
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryArgs args) where T : class
        {
            return _queryPart.Change(data, args);
        }
    }
}
