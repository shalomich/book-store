using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    
    internal class Pagging<T> : DataTransformer<T> where T : class
    {
        public const int MinPageNumber = 1;

        public readonly int _maxPageSize = 60;

        private int _pageSize = int.MaxValue;
        private int _pageNumber = MinPageNumber;

        public Pagging()
        {

        }

        public Pagging(int maxPageSize)
        {
            MaxPageSize = maxPageSize;
        }

        public int PageSize
        {
            init
            {
                if (value <= 0 || value > _maxPageSize)
                    _pageSize = _maxPageSize;
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

        public int MaxPageSize
        {
            init
            {
                if (value <= 0)
                    throw new ArgumentException();

                _maxPageSize = value;
            }
            get
            {
                return _pageSize;
            }
        }

        public void Deconstruct(out int pageSize, out int pageNumber)
        {
            pageSize = PageSize;
            pageNumber = PageNumber;
        }
       
        public override IQueryable<T> Transform(IQueryable<T> data)
        {
            return data.Skip(PageSize * (PageNumber - 1)).Take(PageSize);
        }
    }
    

}
