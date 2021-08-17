
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Args
{
    public record QueryArgs
    {
        public SortingArgs[] Sortings { init; get; }
        public FilterArgs[] Filters { init; get; }
    }
}
