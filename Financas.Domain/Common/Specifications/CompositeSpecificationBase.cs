using System;
using System.Linq.Expressions;

namespace Financas.Domain.Common.Specifications
{
    public abstract class CompositeSpecificationBase<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> expressionLeft;

        private readonly ISpecification<TEntity> expressionRight;

        protected CompositeSpecificationBase(
            ISpecification<TEntity> left,
            ISpecification<TEntity> right)
        {
            expressionLeft = left;
            expressionRight = right;
        }

        public ISpecification<TEntity> Left { get { return expressionLeft; } }

        public ISpecification<TEntity> Right { get { return expressionRight; } }

        public abstract bool IsSatisfiedBy(TEntity obj);

        public abstract Expression<Func<TEntity, bool>> SpecExpression { get; }
    }
}
