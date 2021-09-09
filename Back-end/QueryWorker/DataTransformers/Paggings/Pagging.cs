using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    
    internal abstract record Pagging<T>
    {
        public const int MinPageNumber = 1;
        public const int MaxPageSize = 60;

        private int _pageSize = int.MaxValue;
        private int _pageNumber = MinPageNumber;
        public Pagging(IQueryable<T> data)
        {
            Data = data;
        }

        public IQueryable<T> Data { init; get; }

        public int PageSize
        {
            init
            {
                if (value <= 0)
                    throw new ArgumentException();

                if (value > MaxPageSize)
                    _pageSize = MaxPageSize;
                else _pageSize = value;
            }
            get
            {
                return _pageSize;
            }
        }

        public int PageNumber
        {
            init
            {
                if (value < MinPageNumber)
                    throw new ArgumentException();

                if (value > PageCount && IsEmpty == false)
                    _pageNumber = PageCount;
                else _pageNumber = value;
            }
            get
            {
                return _pageNumber;
            }
        }

        private bool IsEmpty => DataCount == 0;
        public int DataCount => Data.Count();
        public int PageCount => (int)Math.Ceiling(DataCount / (double)PageSize);

        public bool HasNextPage => PageNumber != PageCount && IsEmpty == false;
        public bool HasPreviousPage => PageNumber != MinPageNumber && IsEmpty == false;

        public PaggingMetadata PaggingInfo => new PaggingMetadata(PageSize, PageNumber, DataCount, PageCount, 
            HasNextPage, HasPreviousPage);


        public abstract IQueryable<T> MakePage();

        public static Pagging<T> CreatePagging(IQueryable<T> data, PaggingArgs args) => args.Type switch
        {
            PaggingType.Flip => new FlipPagging<T>(data) { PageSize = args.PageSize, PageNumber = args.PageNumber },
            PaggingType.Scroll => new ScrollPagging<T>(data) { PageSize = args.PageSize, PageNumber = args.PageNumber },
            _ => throw new ArgumentException()
        };
    }
    

}
