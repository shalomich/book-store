
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QueryWorker.Args
{
    public record QueryArgs
    {
        private const int MaxPageSize = 60;
        public SortingArgs[] Sortings { init; get; }
        public FilterArgs[] Filters { init; get; }

        [Required]
        [Range(1,MaxPageSize)]
        public int PageSize { init; get; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int PageNumber { init; get; }

    }
}
