using System;
using System.Linq.Expressions;

namespace Financas.Domain.Common.Specifications
{
    public abstract class SpecificationBase<TEntity> : ISpecification<TEntity>
    {
        private Func<TEntity, bool> compiledExpression;

        protected Func<TEntity, bool> CompiledExpression
        {
            get { return compiledExpression ?? (compiledExpression = SpecExpression.Compile()); }
        }

        /// <summary>
        /// Exige a construção do método que definirá a regra de negócio
        /// </summary>
        public abstract Expression<Func<TEntity, bool>> SpecExpression { get; }

        /// <summary>
        /// Executa de fato a expressão, retornado true/false de acordo com a 
        /// compilação da expressão no objeto submetido.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool IsSatisfiedBy(TEntity entity)
        {
            return CompiledExpression(entity);
        }
    }
}
