using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    public record PaggingInfo(int PageSize, int PageNumber, int DataCount, int PageCount, 
        bool HasNextPage, bool HasPreviousPage);
}
