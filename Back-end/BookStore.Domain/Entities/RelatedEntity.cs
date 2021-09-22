﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public abstract class RelatedEntity : IFormEntity
    {
        public int Id { set; get; }
        public virtual string Name { set; get; }
    }
}