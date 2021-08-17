

using QueryWorker.Args;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Parts
{
    internal class SortingPart : QueryPart
    {
        public override IQueryable<T> Change<T>(IQueryable<T> data, QueryArgs args, QueryConfiguration<T> config) where T : class
        {
            if (args.Sortings != null)
            {
                foreach (SortingArgs sortingArgs in args.Sortings)
                {
                    var sorting = config.Sortings[sortingArgs.PropertyName];
                    sorting.IsAscending = sortingArgs.IsAscending;
                    data = sorting.Execute(data);
                }
            }
            
            return _nextPart?.Change(data, args, config) ?? data;
        }
    }
}
