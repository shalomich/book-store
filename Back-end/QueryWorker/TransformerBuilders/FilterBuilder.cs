using AutoMapper;
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.TransformerBuilders
{
    public class FilterBuilder<TTransformed> : TransformerBuilder<TTransformed, FilterArgs> where TTransformed : class
    {
        protected override MapperConfiguration FromArgsToTransformerMapping => new MapperConfiguration(builder =>
        {
            builder.CreateMap<DataTransformerArgs, DataTransformer<TTransformed>>()
                .IncludeAllDerived();
            builder.CreateMap<FilterArgs, StringFilter<TTransformed>>();
            builder.CreateMap<FilterArgs, NumberFilter<TTransformed>>()
                .ForMember(filter => filter.ComparedValue, mapper =>
                    mapper.MapFrom(args => double.Parse(args.ComparedValue)));
            builder.CreateMap<FilterArgs, CollectionFilter<TTransformed>>()
                .ForMember(filter => filter.ComparedValue, mapper =>
                    mapper.MapFrom(args => args.ComparedValue
                        .Split(',', StringSplitOptions.None)
                        .Select(str => int.Parse(str))));
        });

        internal override DataTransformer<TTransformed> GetFromConfig(FilterArgs args, QueryConfiguration<TTransformed> config)
        {
            return config.GetFilter(args.PropertyName);
        }
    }
}
