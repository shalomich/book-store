using QueryWorker.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.QueryNodes
{
    interface IParsed
    {
        void Accept(IQueryParser parser);
    }
}
