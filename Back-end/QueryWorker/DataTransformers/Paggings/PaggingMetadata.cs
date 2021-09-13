using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Paggings
{
    public record PaggingMetadata(int PageSize, int PageNumber, int CurrentPageDataCount, int DataCount, int PageCount, 
        bool HasNextPage, bool HasPreviousPage);
}
