
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using QueryWorker.Extensions;
using QueryWorker.TransformerBuildNodes;

namespace QueryWorker
{
    public class DataTransformerFacade
    {
        private readonly TransformerBuildNode _queryHead;
        
        private readonly ConfigurationFinder _configurationFinder;

        private List<string> _errorMessages;
        public string[] ErrorMesages => _errorMessages.ToArray();

        public DataTransformerFacade(Assembly assembly)
        {
            _configurationFinder = new ConfigurationFinder(assembly);

            Action<string> errorConservation = message => _errorMessages.Add(message);

            _queryHead = new FilterBuildNode(errorConservation)
                .SetNextNode(new SearchBuildNode(errorConservation)
                .SetNextNode(new SortingBuildNode(errorConservation)));
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryArgs args) where T : class
        {
            _errorMessages = new List<string>();

            if (args.IsQueryEmpty)
                return Transform(data, args as PaggingArgs);

            QueryConfiguration<T> config;
            
            try
            {
                config = _configurationFinder.Find<T>();
            }
            catch(Exception exception)
            {
                _errorMessages.Add(exception.Message);
                return Transform(data,args as PaggingArgs);
            }

            
            var queue = new QueryQueue<T>(); 
            
            _queryHead.FillQueue(queue, args, config);

            return Transform(queue.Transform(data), args as PaggingArgs);
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, PaggingArgs args) where T : class
        {
            return data.Page(args.PageSize, args.PageNumber);
        }
    }
}
