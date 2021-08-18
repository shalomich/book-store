using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.TransformerBuildNodes
{
    internal class FilterBuildNode : TransformerBuildNode
    {
        public FilterBuildNode(Action<string> errorHandler) : base(errorHandler)
        {
        }

        protected override IDataTransformerArgs[] ChooseArgs(QueryArgs args)
        {
            return args.Filters;
        }
    }
}
