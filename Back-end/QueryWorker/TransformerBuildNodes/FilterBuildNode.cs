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
        protected override IDataTransformerArgs[] ChooseArgs(QueryTransformArgs args)
        {
            return args.Filters;
        }
    }
}
