using QueryWorker.Extensions;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using QueryWorker.DataTransformers.Filters.ExpressionCreator;

namespace QueryWorker.DataTransformers.Filters
{
    
    internal sealed record Filter<T> : IDataTransformer<T> where T : class
    {
        private IFilterExpressionCreator<T> ExpressionCreator { get; }
        public string ComparedValue { init; get; }

        public Filter(IFilterExpressionCreator<T> expressionBuilder)
        {
            ExpressionCreator = expressionBuilder ?? throw new ArgumentNullException(nameof(expressionBuilder));
        }

        public IQueryable<T> Transform(IQueryable<T> query)
        {           
            return query.Where(ExpressionCreator.CreateFiltering(ComparedValue));
        }
    }
}
