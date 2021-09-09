using QueryWorker.Args;
using QueryWorker.TransformerBuildNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.TransformerBuildNodes
{
    internal class SearchBuildNode : TransformerBuildNode
    {
        protected override IDataTransformerArgs[] ChooseArgs(QueryTransformArgs args)
        {
            return args.Searches;
        }
    }
}
