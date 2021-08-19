using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Args
{
    internal interface IDataTransformerArgs
    {
        public string PropertyName { init; get; }
    }
}
