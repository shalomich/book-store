﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Publications
{
    public class PublicationType : Entity
    {
        public ISet<Publication> Publications { set; get; }
    }
}