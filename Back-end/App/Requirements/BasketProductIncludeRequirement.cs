using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Requirements
{
    public class BasketProductIncludeRequirement : IIncludeRequirement<Basket>
    {
        public void Include(ref IQueryable<Basket> entities)
        {
            entities = entities
                .Include(basket => basket.BasketProducts)
                .ThenInclude(basketProduct => basketProduct.Product);
        }
    }
}
