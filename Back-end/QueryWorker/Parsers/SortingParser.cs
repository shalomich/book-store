using QueryWorker.QueryNodeParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Parsers
{
    class SortingParser : IQueryParser
    {
        private const char DescendingSymbol = '-';
        public IQueryNodeParams Parse(string query)
        {
            bool isAscending;
            string propertyName;

            if (query.StartsWith(DescendingSymbol))
            {
                isAscending = false;
                query = query.Remove(0, 1);
            }
            else isAscending = true;

            propertyName = query.ToCapitalLetter();
            return new SortingParams(propertyName, isAscending);
        }
    }
}
