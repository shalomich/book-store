using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers
{
    internal class QueryQueue<T> : Queue<IDataTransformer<T>>, IDataTransformer<T> where T : class
    {
        public IQueryable<T> Transform(IQueryable<T> data)
        {
            IDataTransformer<T> transformer;

            while (TryDequeue(out transformer))
                data = transformer.Transform(data);

            return data;
        }
    }
}
