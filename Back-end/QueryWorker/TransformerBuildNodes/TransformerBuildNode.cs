
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.TransformerBuildNodes
{
    internal abstract class TransformerBuildNode
    {
       
        protected TransformerBuildNode _nextNode;

        public TransformerBuildNode SetNextNode(TransformerBuildNode nextNode)
        {
            _nextNode = nextNode;

            return this;
        }

        public void FillQueue<T>(QueryQueue<T> queue, QueryTransformArgs queryArgs, 
            QueryConfiguration<T> config) where T : class
        {
            IDataTransformerArgs[] DataTransformerFacadeArgs = ChooseArgs(queryArgs);

            if (DataTransformerFacadeArgs != null)
            {
                foreach (var args in DataTransformerFacadeArgs)
                {
                    try
                    {
                        var transformer = config.BuildTransformer(args);
                        queue.Enqueue(transformer);
                    }
                    catch(Exception)
                    {
                        continue;
                    }
                }
            }

            _nextNode?.FillQueue(queue, queryArgs, config);
        }

        protected abstract IDataTransformerArgs[] ChooseArgs(QueryTransformArgs args);

    }
}
