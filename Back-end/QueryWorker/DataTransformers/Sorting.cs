﻿
using QueryWorker.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryWorker.DataTransformers
{

    internal sealed record Sorting<T> : IDataTransformer<T> where T : class
    {
        private Expression<Func<T, object>> PropertySelector { get; }
        public bool IsAscending { init; get; } = true;

        public Sorting(Expression<Func<T, object>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public IQueryable<T> Transform(IQueryable<T> query)
        {
            query =  IsAscending ? query.AppendOrderBy(PropertySelector) 
                : query.AppendOrderByDescending(PropertySelector);

            return query;
        }
    }
}
