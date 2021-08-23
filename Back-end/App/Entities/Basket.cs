﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Basket : IEntity
    {
        public int Id { set; get; }
        public virtual User User { set; get; }
        public int UserId { set; get; }
        public virtual ISet<BasketProduct> BasketProducts { set; get; }

    }
}