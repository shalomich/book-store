using App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Requirements
{
    public class UserBasketIncludeRequirement : IIncludeRequirement<User>
    {
        public void Include(ref IQueryable<User> entities)
        {
            entities = entities.Include(user => user.Basket);
        }
    }
}
