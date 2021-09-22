using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public interface IFormEntity : IEntity
    {
        public string Name { set;get; }
    }
}
