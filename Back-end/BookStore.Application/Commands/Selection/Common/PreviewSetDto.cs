using System.Collections.Generic;

namespace BookStore.Application.Commands.Selection.Common
{
    public record PreviewSetDto(IEnumerable<PreviewDto> Previews, int TotalCount);
}
