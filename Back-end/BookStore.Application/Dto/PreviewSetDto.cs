using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record PreviewSetDto(IEnumerable<PreviewDto> Previews, int TotalCount);
}
