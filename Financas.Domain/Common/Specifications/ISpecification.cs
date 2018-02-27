using System;
using System.Linq.Expressions;

namespace Financas.Domain.Common.Specifications
{
    public interface ISpecification<TEntity>
    {
        /// <summary>
        /// Método que será usado para verificar que a entidade informada como parâmetro,
        /// atende aos critérios definidos na expressão
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsSatisfiedBy(TEntity entity);

        /// <summary>
        /// Expressão que conterá a regra de negócio de fato.
        /// </summary>
        Expression<Func<TEntity, bool>> SpecExpression { get; }
    }
}
