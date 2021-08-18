
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
                .SetNextNode(new SortingBuildNode(errorConservation));
        }

        public IQueryable<T> Transform<T>(IQueryable<T> data, QueryArgs args) where T : class
        {
            _errorMessages = new List<string>();

            Func<IQueryable<T>, IQueryable<T>> pagging = data => data.Page(args.PageSize, args.PageNumber);

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
