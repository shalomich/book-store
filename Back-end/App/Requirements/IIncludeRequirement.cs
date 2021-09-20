using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Requirements
{
    public interface IIncludeRequirement<T> where T : IEntity
    {
        public void Include(ref IQueryable<T> entities);
    }
}
