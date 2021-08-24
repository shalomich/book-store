
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QueryWorker.Args
{
    public record QueryTransformArgs
    {
        [Range(1, int.MaxValue)]
        public int PageSize { init; get; } = int.MaxValue;

        [Required]
        [Range(1, int.MaxValue)]
        public int PageNumber { init; get; } = 1;
        public SortingArgs[] Sortings { init; get; }
        public FilterArgs[] Filters { init; get; }
        public SearchArgs[] Searches { init; get; }

        public bool IsQueryEmpty => Sortings == null && Filters == null && Searches == null;
    }
}
