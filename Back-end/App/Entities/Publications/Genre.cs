﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Publications
{
    public class Genre : Entity
    {
        public ISet<GenrePublication> Publications { set; get; }
     
    }
}