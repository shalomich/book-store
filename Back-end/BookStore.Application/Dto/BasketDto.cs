using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record BasketDto
    {
        public int TotalAmount { init; get; }
        public int TotalQuantity { init; get; }
        public BasketProductDto[] BasketProducts { init; get; }
    }
}
