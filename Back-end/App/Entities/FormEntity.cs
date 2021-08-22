using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public abstract class FormEntity : IEntity
    {
        public int Id { set; get; }
        public virtual string Name { set;get; }
    }
}
