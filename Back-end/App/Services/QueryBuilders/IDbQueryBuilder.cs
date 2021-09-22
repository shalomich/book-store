using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.QueryBuilders
{
    public interface IDbQueryBuilder<out T> where T : IEntity
    {
        public IQueryable<T> Build();
    }
}
