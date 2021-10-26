using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    
    internal sealed record Pagging<T> : IDataTransformer<T> where T : class
    {
        public const int MinPageNumber = 1;

        public const int MaxPageSize = 60;

        private int _pageSize = int.MaxValue;
        private int _pageNumber = MinPageNumber;

        public int PageSize
        {
            init
            {
                if (value <= 0 || value > MaxPageSize)
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
                    _pageNumber = MinPageNumber;
                else 
                    _pageNumber = value;
            }
            get
            {
                return _pageNumber;
            }
        }

        public void Deconstruct(out int pageSize, out int pageNumber)
        {
            pageSize = PageSize;
            pageNumber = PageNumber;
        }
       
        public IQueryable<T> Transform(IQueryable<T> data)
        {
            return data.Skip(PageSize * (PageNumber - 1)).Take(PageSize);
        }
    }
    

}
