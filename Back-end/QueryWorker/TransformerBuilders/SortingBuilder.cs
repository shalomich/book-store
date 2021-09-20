using AutoMapper;
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.TransformerBuilders
{
    public class SortingBuilder<TTransformed> : TransformerBuilder<TTransformed, SortingArgs> where TTransformed : class
    {
        protected override MapperConfiguration FromArgsToTransformerMapping =>
            new MapperConfiguration(builder =>
            {
                builder.CreateMap<DataTransformerArgs, DataTransformer<TTransformed>>()
                    .IncludeAllDerived();
                builder.CreateMap<SortingArgs, Sorting<TTransformed>>();
            });
        internal override DataTransformer<TTransformed> GetFromConfig(SortingArgs args, QueryConfiguration<TTransformed> config)
        {
            return config.GetSorting(args.PropertyName);
        }
    }
}
