using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Visitors
{
    public interface IQueryParser
    {
        string Query { set; get; }
        void Parse(Sorting sorting);
        void Parse(Filter filter);
        void Parse(Pagging pagging);
    }
}
