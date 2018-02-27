using System;
using System.Linq.Expressions;

namespace Financas.Domain.Common.Specifications
{
    public class NotSpecification<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> innerSpecification;

        public NotSpecification(ISpecification<TEntity> innerSpecification)
        {
            this.innerSpecification = innerSpecification;
        }

        //public ISpecification<T> InnerSpecification
        //{
        //    get { return innerSpecification; }
        //}

        public bool IsSatisfiedBy(TEntity obj)
        {
            return !innerSpecification.IsSatisfiedBy(obj);
        }

        public Expression<Func<TEntity, bool>> SpecExpression
        {
            get
            {
                ParameterExpression objParam = Expression.Parameter(typeof(TEntity), "obj");

                // Cria nova expressão inverte a condição lógica da expressão original
                Expression<Func<TEntity, bool>> newExpression =
                    Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Not(
                            Expression.Invoke(innerSpecification.SpecExpression, objParam)
                        ),
                        objParam
                    );

                return newExpression;
            }
        }
    }
}
