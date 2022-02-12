
using System.Collections.Generic;

namespace BookStore.WebApi.Areas.Store.ViewModels.Cards
{
    public record ProductPreviewSet(IEnumerable<ProductPreview> Previews, int TotalCount);
}
