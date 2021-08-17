using Microsoft.AspNetCore.Mvc;
using QueryWorker.QueryNodes.Filters;
using System.ComponentModel.DataAnnotations;


namespace QueryWorker.Args
{
    public record FilterArgs
    {
        [Required]
        public string PropertyName { init; get; }
        
        [Required]
        public string Value { init; get; }

        public FilterСomparison Comparison { init; get; }
    }
}
