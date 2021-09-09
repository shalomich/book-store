using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    internal class PaggingFactory
    {
        public Pagging<T> Create<T>(IQueryable<T> data, PaggingArgs args) => args.Type switch
        {
            PaggingType.Flip => new FlipPagging<T>(data) { PageSize = args.PageSize, PageNumber = args.PageNumber },
            PaggingType.Scroll => new ScrollPagging<T>(data) { PageSize = args.PageSize, PageNumber = args.PageNumber },
            _ => throw new ArgumentException()
        };
    }
}
