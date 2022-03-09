using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Extensions
{
    internal static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression ReplaceParameter(this Expression expression,
            ParameterExpression toReplace,
            Expression newExpression)
        {
            return new ParameterReplaceVisitor(toReplace, newExpression)
                .Visit(expression);
        }

        public static Expression<Func<TSource, TResult>> Compose<TSource, TIntermediate, TResult>(
            this Expression<Func<TSource, TIntermediate>> first,
            Expression<Func<TIntermediate, TResult>> second)
        {
            var param = Expression.Parameter(typeof(TSource));
            var intermediateValue = first.Body.ReplaceParameter(first.Parameters[0], param);
            var body = second.Body.ReplaceParameter(second.Parameters[0], intermediateValue);
            return Expression.Lambda<Func<TSource, TResult>>(body, param);
        }
    }

    internal class ParameterReplaceVisitor : ExpressionVisitor
    {
        private ParameterExpression from;
        private Expression to;
        public ParameterReplaceVisitor(ParameterExpression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == from ? to : node;
        }
    }
}
