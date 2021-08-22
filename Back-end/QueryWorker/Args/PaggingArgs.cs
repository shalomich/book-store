using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Args
{
    public record PaggingArgs
    {
        [Range(1, int.MaxValue)]
        public int PageSize { init; get; } = int.MaxValue;

        [Required]
        [Range(1, int.MaxValue)]
        public int PageNumber { init; get; } = 1;
    }
}
