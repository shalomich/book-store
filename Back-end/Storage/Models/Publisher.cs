using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Publisher : Entity
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public List<Publication> Publications { set; get; }
    }
}
