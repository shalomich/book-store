using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Args
{
    public record SearchArgs
    {
        [Required]
        public string PropertyName { init; get; }

        [Required]
        public string ComparedValue { init; get; }

        [Range(Search<object>.MinSearchDepth, Search<object>.MaxSearchDepth)]
        public int? SearchDepth { init; get; }

    }
}
