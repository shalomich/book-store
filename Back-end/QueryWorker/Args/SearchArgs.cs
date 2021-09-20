using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Args
{
    public record SearchArgs : DataTransformerArgs
    {
        [Required]
        public string PropertyName { init; get; }

        [Required]
        public string ComparedValue { init; get; }

        public int SearchDepth { init; get; }

    }
}
