using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record OptionParameters
    {
        [Required]
        public PaggingArgs Pagging { get; init; }
        public FilterArgs[] Filters { get; init; } = new FilterArgs[0];
        public SortingArgs[] Sortings { get; init; } = new SortingArgs[0];

        public void Deconstruct(out PaggingArgs pagging, out FilterArgs[] filters, out SortingArgs[] sortings)
        {
            pagging = Pagging;
            filters = Filters;
            sortings = Sortings;
        }
    }
}
