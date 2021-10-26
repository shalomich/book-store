using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers
{
    public interface IDataTransformer<T> where T : class
    {
        IQueryable<T> Transform(IQueryable<T> data);
    }
}
