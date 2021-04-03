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

        public event Action<string, string> Accepted;
        public event Action<string, string> Crashed;

        public int PageSize 
        {
            set
            {
                if (value < _minPageSize || value > _maxPageSize)
                {
                    _pageSize = _maxPageSize;
                    Crashed?.Invoke("pageSize",$"curent pageSize is unvalid {value}, use default pageSize {_maxPageSize}");
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
                    Crashed?.Invoke("pageNumber", $"curent pageNumber is unvalid ({value}), use default pageNumber ({_minPageSize})");
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

        public int CalcPageCount<T>(IQueryable<T> query) => (int)Math.Ceiling(query.Count() / (double)PageSize);
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
            int pageCount = CalcPageCount(query);
            bool hasNextPage = PageNumber < pageCount;
            bool hasPreviousPage = PageNumber > 1;

            Accepted?.Invoke(nameof(pageCount), pageCount.ToString());
            Accepted?.Invoke(nameof(hasNextPage), hasNextPage.ToString());
            Accepted?.Invoke(nameof(hasPreviousPage), hasPreviousPage.ToString());

            int pageNumber = PageNumber;
            if (PageNumber > pageCount)
            {
                string error = $"pageNumber({pageNumber}) less than pageCount ({pageCount}), used last page";
                Crashed?.Invoke(nameof(PageNumber), $"{error}, {this.ToString()}");
                pageNumber = pageCount;
            }

            return query.Skip(PageSize * (pageNumber - 1)).Take(PageSize);
        }

        public void Accept(IQueryParser parser)
        {
            parser.Parse(this);
        }

        public override string ToString()
        {
            return $"Pagging(pageSize: {PageSize},pageNumber: {PageNumber})";
        }
    }
}
