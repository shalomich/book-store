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
    public class SearchBuilder<TTransformed> : TransformerBuilder<TTransformed, SearchArgs> where TTransformed : class
    {
        protected override MapperConfiguration FromArgsToTransformerMapping =>
            new MapperConfiguration(builder =>
            {
                builder.CreateMap<DataTransformerArgs, DataTransformer<TTransformed>>()
                    .IncludeAllDerived();
                builder.CreateMap<SearchArgs, Search<TTransformed>>();
            });

        internal override DataTransformer<TTransformed> GetFromConfig(SearchArgs args, QueryConfiguration<TTransformed> config)
        {
            return config.GetSearch(args.PropertyName);
        }
    }
}
