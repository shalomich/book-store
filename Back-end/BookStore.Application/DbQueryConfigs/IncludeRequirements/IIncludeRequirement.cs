using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.IncludeRequirements
{
    public interface IIncludeRequirement<T> where T : IEntity
    {
        public void Include(ref IQueryable<T> entities);
    }
}
