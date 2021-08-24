
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QueryWorker.Args
{
    public record QueryTransformArgs
    {
        public PaggingArgs Pagging { init; get; } = new PaggingArgs();
        public SortingArgs[] Sortings { init; get; }
        public FilterArgs[] Filters { init; get; }
        public SearchArgs[] Searches { init; get; }

        public bool IsQueryEmpty => Sortings == null && Filters == null && Searches == null;
    }
}
