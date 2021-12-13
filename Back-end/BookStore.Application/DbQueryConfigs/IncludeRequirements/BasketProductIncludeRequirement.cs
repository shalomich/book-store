using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.IncludeRequirements
{
    public class BasketProductIncludeRequirement : IIncludeRequirement<BasketProduct>
    {
        public void Include(ref IQueryable<BasketProduct> entities)
        {
            entities = entities
                .Include(basketProduct => basketProduct.Product)
                .ThenInclude(product => product.Album)
                .ThenInclude(album => album.Images);
        }
    }
}
