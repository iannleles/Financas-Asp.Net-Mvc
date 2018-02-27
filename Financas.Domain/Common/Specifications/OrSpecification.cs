using System;
using System.Linq.Expressions;

namespace Financas.Domain.Common.Specifications
{
    public class OrSpecification<TEntity> : CompositeSpecificationBase<TEntity>
    {
        public OrSpecification(
            ISpecification<TEntity> left,
            ISpecification<TEntity> right)
            : base(left, right)
        {
        }

        public override bool IsSatisfiedBy(TEntity obj)
        {
            return Left.IsSatisfiedBy(obj) || Right.IsSatisfiedBy(obj);
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

                var newExpression = Left.SpecExpression.Or(Right.SpecExpression);

                //Expression<Func<TEntity, bool>> newExpression =
                //    Expression.Lambda<Func<TEntity, bool>>(
                //        Expression.OrElse(
                //            Expression.Invoke(Left.SpecExpression, objParam),
                //            Expression.Invoke(Right.SpecExpression, objParam)
                //        ),
                //        objParam
                //    );

                return newExpression;
            }
        }
    }
}
