﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Publications
{
    public class AgeLimit : Entity
    {
        public ISet<Publication> Publications { set; get; }
    }
}