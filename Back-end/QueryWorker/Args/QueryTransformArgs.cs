
using QueryWorker.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QueryWorker.Args
{
    public record QueryTransformArgs : IEnumerable<DataTransformerArgs>
    {
        public SortingArgs[] Sortings { init; get; }
        public FilterArgs[] Filters { init; get; }
        public SearchArgs[] Searches { init; get; }

        public bool IsQueryEmpty => Sortings == null && Filters == null && Searches == null;

        public IEnumerator<DataTransformerArgs> GetEnumerator()
        {
            yield return Pagging;

            foreach (var filterArgs in Filters.EmptyIfNull())
                yield return filterArgs;
            
            foreach (var sortingArgs in Sortings.EmptyIfNull())
                yield return sortingArgs;
            
            foreach (var searchArgs in Searches.EmptyIfNull())
                yield return searchArgs;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
