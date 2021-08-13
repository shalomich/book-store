using QueryWorker.Configurations;
using QueryWorker.Parsers;
using QueryWorker.QueryNodeParams;
using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Factories
{
    class SortingFactory : QueryNodeFactory
    {
        private readonly ConfigurationFinder _configurationFinder;
        
        private readonly SortingParser _parser = new SortingParser(); 
        protected override IQueryParser Parser => _parser;

        public SortingFactory(ConfigurationFinder configurationFinder)
        {
            _configurationFinder = configurationFinder ?? throw new ArgumentNullException(nameof(configurationFinder));
        }

        public override IQueryNode<T> Create<T>(string query) where T : class
        {
            var parameters = GetParams(query) as SortingParams;

            var configuration = _configurationFinder.Find<T,SortingConfiguration<T>>();

            var propertySelector = configuration.GetPropertySelector(parameters.PropertyName);

            return new Sorting<T>(propertySelector, parameters.IsAscending);
        }
    }
}
