﻿
using QueryWorker.DataTransformers.Filters;
using System.ComponentModel.DataAnnotations;


namespace QueryWorker.Args
{
    public record FilterArgs
    {
        [Required]
        public string PropertyName { init; get; }
        
        [Required]
        public string ComparedValue { init; get; }
    }
}
