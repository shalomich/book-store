using QueryWorker.Parsers;
using QueryWorker.QueryNodeParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Parts
{
    class SortingPart : QueryPart
    {
        public SortingPart(ConfigurationFinder configurationFinder) : base(configurationFinder)
        {
        }

        public override IQueryable<T> Change<T>(IQueryable<T> data, QueryArgs args) where T : class
        {
            var queryConfig = _configurationFinder.Find<T>();
            var parser = new SortingParser();
            foreach (string sortingQuery in args.Sorting)
            {
                var sortingArgs = parser.Parse(sortingQuery) as SortingArgs;
                var sorting = queryConfig.Sortings[sortingArgs.PropertyName];
                sorting.IsAscending = sortingArgs.IsAscending;
                data = sorting.Execute(data);
            }

            return _nextPart?.Change(data, args) ?? data;
        }
    }
}
