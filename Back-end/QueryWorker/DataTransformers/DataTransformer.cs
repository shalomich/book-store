using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers
{
    public abstract class DataTransformer<T> where T : class
    {
        public abstract IQueryable<T> Transform(IQueryable<T> data);
    }
}
