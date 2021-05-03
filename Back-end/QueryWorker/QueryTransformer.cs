﻿using Microsoft.Extensions.Configuration;
using QueryWorker.QueryNodes;
using QueryWorker.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryWorker.Factories;

namespace QueryWorker
{
    public class QueryTransformer<T>
    {
        private FilterFactory _filterFactory;
        private SortingFactory _sortingFactory;
        private PaggingFactory _paggingFactory;
        private IQueryParser _parser;
        private Informer _informer;

        public Informer Informer => _informer;
        public QueryTransformer(IConfiguration configuration, IQueryParser parser)
        {
            _parser = parser;
            IConfigurationSection querySection = configuration.GetSection("query");
            string typeName = typeof(T).Name;

            _paggingFactory = querySection.GetSection("pagging").Get<PaggingFactory>();
            _sortingFactory = querySection.GetSection($"sorting:{typeName}").Get<SortingFactory>();
            _filterFactory = querySection.GetSection($"filter:{typeName}").Get<FilterFactory>();
            
            _informer = new Informer();
            _informer.Subscribe(_parser);
        }

        public IQueryable<T> Transform(IQueryable<T> data, QueryParams parameters)
        {
            var queryQueue = new Queue<IQueryNode>();
            
            foreach (string filterQuery in parameters.Filter)
                queryQueue.Enqueue(CreateNode(_filterFactory, filterQuery));
            
            foreach (string sortingQuery in parameters.Sorting)
                queryQueue.Enqueue(CreateNode(_sortingFactory, sortingQuery));

            queryQueue.Enqueue(CreateNode(_paggingFactory, parameters.Pagging));

            while (queryQueue.TryDequeue(out IQueryNode node) != false)
                data = node.Execute(data);
            
            return data;
        }

        private IQueryNode CreateNode(IQueryNodeFactory factory, string query)
        {
            IQueryNode queryNode = factory.Create();
            _informer.Subscribe(queryNode);
           
            _parser.Query = query;
            ((IParsed)queryNode).Accept(_parser);

            return queryNode;
        }
    }
}