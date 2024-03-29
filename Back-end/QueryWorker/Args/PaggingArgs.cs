﻿using QueryWorker.DataTransformers.Paggings;
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
        [Required]
        public int PageSize { init; get; }

        [Range(1, int.MaxValue)]
        [Required]
        public int PageNumber { init; get; }
        
    }
}
