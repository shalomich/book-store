using Abp.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.DataTransformers.Filters.ExpressionCreator
{
    internal class SpecificationExpressionCreator<T> : IFilterExpressionCreator<T> where T : class
    {
        private ISpecification<T> Specification { get; }

        public SpecificationExpressionCreator(ISpecification<T> specification)
        {
            Specification = specification;
        }

        public Expression<Func<T, bool>> CreateFiltering(string comparedValue)
        {
            bool availableStatus;

            if (bool.TryParse(comparedValue, out availableStatus) == false)
                availableStatus = true;

            return availableStatus ? Specification.ToExpression() : Specification.Not().ToExpression();
        }
    }
}
