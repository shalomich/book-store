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
        public SearchBuildNode(Action<string> errorHandler) : base(errorHandler)
        {
        }

        protected override IDataTransformerArgs[] ChooseArgs(QueryArgs args)
        {
            return args.Searches;
        }
    }
}
