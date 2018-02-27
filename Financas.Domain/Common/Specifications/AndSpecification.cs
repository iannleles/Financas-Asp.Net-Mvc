using System;
using System.Linq;
using System.Linq.Expressions;

namespace Financas.Domain.Common.Specifications
{
    public class AndSpecification<TEntity> : CompositeSpecificationBase<TEntity>
    {
        public AndSpecification(
            ISpecification<TEntity> left,
            ISpecification<TEntity> right)
            : base(left, right)
        {
        }

        public override bool IsSatisfiedBy(TEntity obj)
        {
            return Left.IsSatisfiedBy(obj) && Right.IsSatisfiedBy(obj);
        }

        public override Expression<Func<TEntity, bool>> SpecExpression
        {
            get
            {
                if (Left == null)
                {
                    return Right.SpecExpression;
                }

                if (Right == null)
                {
                    return Left.SpecExpression;
                }

                ParameterExpression objParam = Expression.Parameter(typeof(TEntity), "obj");

                var newExpression = Left.SpecExpression.And(Right.SpecExpression);

                //Expression<Func<TEntity, bool>> newExpression =
                //    Expression.Lambda<Func<TEntity, bool>>(
                //        Expression.AndAlso(
                //            Left.SpecExpression.Body,
                //            Right.SpecExpression.Body
                //        ),
                //        Left.SpecExpression.Parameters.Single()
                //    );

                return newExpression;
            }
        }
    }
}
