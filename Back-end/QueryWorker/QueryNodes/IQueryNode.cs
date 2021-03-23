using QueryWorker.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryWorker
{
    public interface IQueryNode
    {
        IQueryable<T> Execute<T>(IQueryable<T> query);
    }
}
