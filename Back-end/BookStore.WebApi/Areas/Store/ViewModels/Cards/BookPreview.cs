using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.ViewModels.Cards
{
    public record BookPreview : ProductPreview
    {
        public string AuthorName { init; get; }
        public string PublisherName { init; get; }
    }
}
