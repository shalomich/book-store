

using QueryWorker.Args;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.TransformerBuildNodes
{
    internal class SortingBuildNode : TransformerBuildNode
    {
        public SortingBuildNode(Action<string> errorHandler) : base(errorHandler)
        {
        }

        protected override IDataTransformerArgs[] ChooseArgs(QueryTransformArgs args)
        {
            return args.Sortings;
        }
    }
}
