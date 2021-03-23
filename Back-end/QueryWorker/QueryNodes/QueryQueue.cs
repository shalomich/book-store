using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryWorker.QueryNodes
{
    class QueryQueue : Queue<IQueryNode>, IQueryNode
    {
        public IQueryable<T> Execute<T>(IQueryable<T> query)
        {
            while (TryDequeue(out IQueryNode currentNode) != false)
            {
                try
                {
                    query = currentNode.Execute(query);
                }
                catch (Exception)
                {

                }
            }

            return query;
        }
    }
}
