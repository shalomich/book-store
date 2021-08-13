using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace QueryWorker
{
    public class Pagging<T> : IQueryNode<T> where T : class
    {
        private const int _minPageSize = 1;
        private const int _minPageNumber = 1;
        
        private int _pageSize;
        private int _pageNumber;
        private int _maxPageSize = int.MaxValue;

        public int PageSize 
        {
            set
            {
                if (value < _minPageSize || value > _maxPageSize)
                {
                    _pageSize = _maxPageSize;
                }
                else _pageSize = value;
            }
            get
            {
                return _pageSize;
            }
        }

        public int PageNumber
        {
            set
            {
                if (value < _minPageNumber)
                {
                    _pageNumber = _minPageNumber;
                }
                else _pageNumber = value;
            }
            get
            {
                return _pageNumber;
            }
        }

        public int MaxPageSize 
        {
            set
            {
                if (value < int.MaxValue && value > _minPageSize)
                {
                    _maxPageSize = value;
                }
            }
            get
            {
                return _maxPageSize;
            }
        }

        public int CalcPageCount(IQueryable<T> query) => (int)Math.Ceiling(query.Count() / (double)PageSize);
        public Pagging()
        {
            PageSize = _maxPageSize;
            PageNumber = _minPageNumber;
        }

        public Pagging(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public IQueryable<T> Execute(IQueryable<T> query)
        {
            int pageCount = CalcPageCount(query);
            bool hasNextPage = PageNumber < pageCount;
            bool hasPreviousPage = PageNumber > 1;

            int pageNumber = PageNumber;
            if (PageNumber > pageCount)
                pageNumber = pageCount;
            

            return query.Skip(PageSize * (pageNumber - 1)).Take(PageSize);
        }
    }
}
