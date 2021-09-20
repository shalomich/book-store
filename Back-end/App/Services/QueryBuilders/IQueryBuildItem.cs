using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.QueryBuilders
{
    public interface IQueryBuildItem<T> where T : IEntity
    {
        public void AddQuery(ref IQueryable<T> entities);
    }
}
