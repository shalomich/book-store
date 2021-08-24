
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

        private List<string> _errorMessages = new List<string>();

        public string[] ErrorMesages => _errorMessages.ToArray();

        public DataTransformerFacade(ConfigurationFinder configurationFinder)
        {
            _configurationFinder = configurationFinder;

            Action<string> errorConservation = message => _errorMessages.Add(message);

            _queryHead = new FilterBuildNode(errorConservation)
                .SetNextNode(new SearchBuildNode(errorConservation)
                .SetNextNode(new SortingBuildNode(errorConservation)));
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryTransformArgs args) where T : class
        {
            Func<IQueryable<T>,IQueryable<T>> pagging = data => data.Page(args.PageSize, args.PageNumber);

            if (args.IsQueryEmpty)
                return pagging(data);

            QueryConfiguration<T> config;
            
            try
            {
                config = _configurationFinder.Find<T>();
            }
            catch(Exception exception)
            {
                _errorMessages.Add(exception.Message);
                return pagging(data);
            }

            
            var queue = new QueryQueue<T>(); 
            
            _queryHead.FillQueue(queue, args, config);

            return pagging(queue.Transform(data));
        }
    }
}
