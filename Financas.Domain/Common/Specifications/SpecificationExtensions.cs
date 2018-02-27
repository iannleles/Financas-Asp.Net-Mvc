using System;
using System.Linq;
using System.Linq.Expressions;

namespace Financas.Domain.Common.Specifications
{
    public static class SpecificationExtensions
    {
        public static ISpecification<TEntity> And<TEntity>(
            this ISpecification<TEntity> left,
            ISpecification<TEntity> right)
        {
            return new AndSpecification<TEntity>(left, right);
        }

        public static ISpecification<TEntity> Or<TEntity>(
            this ISpecification<TEntity> left,
            ISpecification<TEntity> right)
        {
            return new OrSpecification<TEntity>(left, right);
        }

        public static ISpecification<TEntity> Negate<TEntity>(
            this ISpecification<TEntity> inner)
        {
            return new NotSpecification<TEntity>(inner);
        }

        private static Expression<TEntity> Compose<TEntity>(
            this Expression<TEntity> firstExpression,
            Expression<TEntity> secondExpression,
            Func<Expression, Expression, Expression> merge)
        {
            // Constrói um Mapa de Parâmetros
            // a partir dos parâmetros da segunda para a primeira expressão
            var map = firstExpression.Parameters.Select((firstParameters, index) => new
            {
                firstParameters,
                secondParameters = secondExpression.Parameters[index]
            })
            .ToDictionary(
                parameter => parameter.secondParameters,
                parameter => parameter.firstParameters
            );

            // Coleta a expressão de fato da PRIMEIRA EXPRESSÃO
            Expression firstBody = firstExpression.Body;

            // Obtémm a expressão de fato da SEGUNDA EXPRESSÃO
            // Substitui parâmetros da segunda expressão Lambda com
            // os parâmetros da primeira expressão
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, secondExpression.Body);

            // Aplica a composição de Expressões a partir dos parâmetros
            // da primeira expressão
            return Expression.Lambda<TEntity>(merge(firstBody, secondBody), firstExpression.Parameters);
        }

        public static Expression<Func<TEntity, bool>> And<TEntity>(
            this Expression<Func<TEntity, bool>> first,
            Expression<Func<TEntity, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<TEntity, bool>> Or<TEntity>(
            this Expression<Func<TEntity, bool>> first,
            Expression<Func<TEntity, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
    }
}
