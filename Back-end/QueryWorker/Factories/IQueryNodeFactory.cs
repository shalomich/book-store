using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker.Factories
{
    interface IQueryNodeFactory
    {
        IQueryNode Create();
    }
}
