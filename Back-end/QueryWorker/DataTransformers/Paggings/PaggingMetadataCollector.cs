using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.TransformerBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    public class PaggingMetadataCollector<T> where T : class
    {
        public PaggingMetadata Collect(PaggingArgs args, IQueryable<T> query)
        {
            var pagging = new Pagging<T>() { PageSize = args.PageSize, PageNumber = args.PageNumber };
            
            var (pageSize, pageNumber) = pagging;

            int dataCount = query.Count();

            int currentPageDataCount = pagging.Transform(query).Count();

            int pageCount = (int)Math.Ceiling(dataCount / (double)pageSize);

            return new PaggingMetadata(
                PageSize: pageSize,
                PageNumber: pageNumber,
                DataCount: dataCount,
                CurrentPageDataCount: currentPageDataCount,
                PageCount: pageCount
            );
        }
    }
}
