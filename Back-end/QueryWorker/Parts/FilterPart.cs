using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.QueryNodes;
using QueryWorker.QueryNodes.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Parts
{
    class FilterPart : QueryPart
    {
        public override IQueryable<T> Change<T>(IQueryable<T> data, QueryArgs args, QueryConfiguration<T> config) where T : class
        {

            if (args.Filters != null)
            {
                foreach (FilterArgs filterArgs in args.Filters)
                {
                    NumberFilter<T> filter;
                    filter = config.NumberFilters[filterArgs.PropertyName];
                    filter.ComparedValue = filterArgs.Value;
                    filter.Comparison = filterArgs.Comparison;
                    data = filter.Execute(data);
                }
            }

            return _nextPart?.Change(data, args, config) ?? data;
        }
    }
}
