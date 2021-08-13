using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.QueryNodes
{
    internal interface IQueryNode<T> where T : class
    {
        IQueryable<T> Execute(IQueryable<T> data);
    }
}
