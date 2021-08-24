using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    internal record ScrollPagging<T> : Pagging<T>
    {
        public ScrollPagging(IQueryable<T> data) : base(data)
        {
        }

        public override IQueryable<T> MakePage()
        {
            return Data.Take(PageSize * PageNumber);
        }
    }
}
