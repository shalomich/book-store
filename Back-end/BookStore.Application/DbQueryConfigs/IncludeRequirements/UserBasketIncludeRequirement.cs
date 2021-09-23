using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.IncludeRequirements
{
    public class UserBasketIncludeRequirement : IIncludeRequirement<User>
    {
        public void Include(ref IQueryable<User> entities)
        {
            entities = entities.Include(user => user.Basket);
        }
    }
}
