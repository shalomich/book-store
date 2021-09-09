
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
        public PaggingMetadata PaggingInfo { private set; get; } 

        public DataTransformerFacade(ConfigurationFinder configurationFinder)
        {
            _configurationFinder = configurationFinder;

            _queryHead = new FilterBuildNode()
                .SetNextNode(new SearchBuildNode()
                .SetNextNode(new SortingBuildNode()));
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryTransformArgs args) where T : class
        {
            var pagging = Pagging<T>.CreatePagging(data, args.Pagging);
            PaggingInfo = pagging.PaggingInfo;

            if (args.IsQueryEmpty)
                return data;

            QueryConfiguration<T> config;
            
            config = _configurationFinder.Find<T>();
           
            var queue = new QueryQueue<T>(); 
            
            _queryHead.FillQueue(queue, args, config);

            pagging = pagging with { Data = queue.Transform(data) };
            PaggingInfo = pagging.PaggingInfo;

            return pagging.MakePage();
        }
    }
}
