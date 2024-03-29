﻿using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Services.DbQueryBuilders.DbQueryBuildItems
{
    public interface IQueryBuildItem<T> where T : IEntity
    {
        public void AddQuery(ref IQueryable<T> entities);
    }
}
