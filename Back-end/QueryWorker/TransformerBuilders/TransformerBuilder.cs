
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
    public abstract class TransformerBuilder<TTransformed, TArgs> where TTransformed : class
        where TArgs : DataTransformerArgs
    {
        protected virtual MapperConfiguration FromArgsToTransformerMapping { get; }
        private IMapper Mapper => FromArgsToTransformerMapping.CreateMapper();

        internal DataTransformer<TTransformed> Build(TArgs args, QueryConfiguration<TTransformed> config)
        {
            return Mapper.Map(args, GetFromConfig(args, config));      
        }

        internal abstract DataTransformer<TTransformed> GetFromConfig(TArgs args, 
            QueryConfiguration<TTransformed> config);
    }
}
