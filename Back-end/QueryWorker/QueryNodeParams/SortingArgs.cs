using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.QueryNodeParams
{
    internal record SortingArgs(string PropertyName, bool IsAscending = true) : IQueryNodeArgs;
}
