using QueryWorker.QueryNodeParams;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Parsers
{
    internal interface IQueryParser
    {
        public IQueryNodeArgs Parse(string query); 
    }
}
