using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public class PreviewDto
    {
        public int Id { init; get; }
        public string Name { init; get; }
        public int Cost { init; get; }
        public int? DiscountPercentage { init; get; }
        public ImageDto TitleImage { init; get; }
        public bool? IsInBasket { set; get; }
        public string AuthorName { init; get; }
        public string PublisherName { init; get; }
    }
}
