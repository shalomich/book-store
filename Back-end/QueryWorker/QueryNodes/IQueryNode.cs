using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.QueryNodes
{
    internal interface IQueryNode 
    {
        IQueryable<T> Execute<T>(IQueryable<T> data, QueryConfiguration config) where T : class;
    }
}
