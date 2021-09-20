using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public Basket Basket { set; get; }

        public Basket CreateBasket()
        {
            return new Basket { User = this };
        }
    }
}
