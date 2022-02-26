using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record SearchParameters : OptionParameters
    {
        public SearchArgs Search { get; init; }

        public void Deconstruct(out SearchArgs search, out PaggingArgs pagging, out FilterArgs[] filters, out SortingArgs[] sortings)
        {
            search = Search;

            base.Deconstruct(out pagging, out filters, out sortings);
        }
    }
}
