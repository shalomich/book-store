﻿using QueryWorker.Configurations;
using QueryWorker.QueryNodeParams;
using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QueryWorker
{

    internal class Sorting<T> : IQueryNode<T> where T : class
    {
        private readonly Expression<Func<T, object>> _propertySelector;
        public bool IsAscending { set; get; } = true;
        public Sorting(Expression<Func<T, object>> propertySelector)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public IQueryable<T> Execute(IQueryable<T> query)
        {
            query =  IsAscending == true ? query.AppendOrderBy(_propertySelector).AsQueryable() 
                : query.AppendOrderByDescending(_propertySelector);

            return query;
        }
    }
}
