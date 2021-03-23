using QueryWorker.QueryNodes;
using QueryWorker.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryWorker
{
    public class Pagging : IQueryNode, IParsed
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
            set
            {
                if (value < _minPageNumber)
                    _pageNumber = _minPageNumber;
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
                    _maxPageSize = value;
            }
            get
            {
                return _maxPageSize;
            }
        }

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

        public IQueryable<T> Execute<T>(IQueryable<T> query)
        {
            return query.Skip(PageSize * (PageNumber - 1)).Take(PageSize);
        }

        public void Accept(IQueryParser parser)
        {
            parser.Parse(this);
        }
    }
}
