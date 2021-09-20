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
        public PaggingMetadata Collect(PaggingArgs args, IQueryable<T> objects)
        {
            var pagging = new Pagging<T>() { PageSize = args.PageSize, PageNumber = args.PageNumber };
            
            var (pageSize, pageNumber) = pagging;

            return new PaggingMetadata(
                PageSize: pageSize,
                PageNumber: pageNumber,
                DataCount: FindDataCount(objects),
                CurrentPageDataCount: FindDataCount(pagging.Transform(objects)),
                PageCount: FindPageCount(objects, pageSize),
                HasNextPage: HasNextPage(objects, pageSize, pageNumber),
                HasPreviousPage: HasPreviousPage(objects, pageNumber, Pagging<T>.MinPageNumber)
            );
        }

        private int FindDataCount(IQueryable<T> objects) => objects.Count();
        private int FindPageCount(IQueryable<T> objects, int pageSize) 
            => (int)Math.Ceiling(FindDataCount(objects) / (double) pageSize);

        private bool IsEmpty(IQueryable<T> objects) => FindDataCount(objects) == 0;
        private bool HasNextPage(IQueryable<T> objects, int pageSize, int pageNumber) 
            => pageNumber != FindPageCount(objects, pageSize) && IsEmpty(objects) == false;
        private bool HasPreviousPage(IQueryable<T> objects, int pageNumber, int minPageNumber) 
            => pageNumber != minPageNumber && IsEmpty(objects) == false;

    }
}
