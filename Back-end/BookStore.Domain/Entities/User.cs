using BookStore.Domain.Entities.Products;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public string Address { get; set; }
        public ISet<BasketProduct> BasketProducts { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public ISet<Mark> Marks { get; set; }
    }
}
