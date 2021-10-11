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
        public PaggingMetadata Collect(int dataCount, PaggingArgs args, IQueryable<T> objects)
        {
            var pagging = new Pagging<T>() { PageSize = args.PageSize, PageNumber = args.PageNumber };
            
            var (pageSize, pageNumber) = pagging;

            return new PaggingMetadata(
                PageSize: pageSize,
                PageNumber: pageNumber,
                DataCount: FindDataCount(objects),
                CurrentPageDataCount: FindDataCount(objects),
                PageCount: FindPageCount(dataCount, pageSize),
                HasNextPage: HasNextPage(dataCount, pageSize, pageNumber, objects),
                HasPreviousPage: HasPreviousPage(pageNumber, Pagging<T>.MinPageNumber, objects)
            );
        }

        private int FindDataCount(IQueryable<T> objects) => objects.Count();
        private int FindPageCount(int dataCount, int pageSize) 
            => (int)Math.Ceiling(dataCount / (double) pageSize);

        private bool IsEmpty(IQueryable<T> objects) => FindDataCount(objects) == 0;
        private bool HasNextPage(int dataCount, int pageSize, int pageNumber, IQueryable<T> objects) 
            => pageNumber != FindPageCount(dataCount, pageSize) && IsEmpty(objects) == false;
        private bool HasPreviousPage(int pageNumber, int minPageNumber, IQueryable<T> objects) 
            => pageNumber != minPageNumber && IsEmpty(objects) == false;

    }
}
