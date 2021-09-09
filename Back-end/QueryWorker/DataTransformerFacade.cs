
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Paggings;
using QueryWorker.Extensions;
using QueryWorker.TransformerBuildNodes;

namespace QueryWorker
{
    public class DataTransformerFacade
    {
        private readonly TransformerBuildNode _queryHead;
        
        private readonly ConfigurationFinder _configurationFinder;

        private readonly PaggingFactory _paggingFactory;
        public DataTransformerFacade(ConfigurationFinder configurationFinder)
        {
            _configurationFinder = configurationFinder;

            _queryHead = new FilterBuildNode()
                .SetNextNode(new SearchBuildNode()
                .SetNextNode(new SortingBuildNode()));

            _paggingFactory = new PaggingFactory();
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryTransformArgs args) where T : class
        {
            QueryConfiguration<T> config = _configurationFinder.Find<T>();
           
            var queue = new QueryQueue<T>(); 
            
            _queryHead.FillQueue(queue, args, config);

            var pagging = _paggingFactory.Create(data, args.Pagging);

            return pagging.MakePage();
        }

        public QueryMetadata GetQueryMetadata<T>(IQueryable<T> data, QueryTransformArgs args) where T : class
        {
            QueryConfiguration<T> config = _configurationFinder.Find<T>();

            var pagging = _paggingFactory.Create(data, args.Pagging);

            var metaData = new QueryMetadata(pagging.Metadata);

            _queryHead.CheckBrokenDataTransformers(metaData, args, config);

            return metaData;
        }
    }
}
