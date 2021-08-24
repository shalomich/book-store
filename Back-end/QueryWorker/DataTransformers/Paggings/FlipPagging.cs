using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    internal record FlipPagging<T> : Pagging<T>
    {
        public FlipPagging(IQueryable<T> data) : base(data)
        {
        }

        public override IQueryable<T> MakePage()
        {
            return Data.Skip(PageSize * (PageNumber - 1)).Take(PageSize);
        }
    }
}
