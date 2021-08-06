using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Publisher : Entity
    {
        public string Description { set; get; }
        public List<Publication> Publications { set; get; }
    }
}
