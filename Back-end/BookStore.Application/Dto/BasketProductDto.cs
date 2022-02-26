using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public class BasketProductDto
    {
        public int Id { init; get; }
        public string Name { init; get; }
        public int Cost { init; get; }
        public int Quantity { init; get; }
        public int ProductId { init; get; }
        public ImageDto TitleImage { init; get; }
    }
}
