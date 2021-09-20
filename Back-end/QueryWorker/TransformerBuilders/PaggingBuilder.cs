using AutoMapper;
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.TransformerBuilders
{
    public class PaggingBuilder<TTransformed> : TransformerBuilder<TTransformed, PaggingArgs> where TTransformed : class
    {
        protected override MapperConfiguration FromArgsToTransformerMapping =>
            new MapperConfiguration(builder =>
            {
                builder.CreateMap<DataTransformerArgs, DataTransformer<TTransformed>>()
                    .IncludeAllDerived();
                builder.CreateMap<PaggingArgs, Pagging<TTransformed>>();
            });

        internal override DataTransformer<TTransformed> GetFromConfig(PaggingArgs args, QueryConfiguration<TTransformed> config)
        {
            return config.GetPagging();
        }
    }
}
